﻿using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using LibLSLCC.CodeValidator;
using LibLSLCC.CodeValidator.Components;
using LibLSLCC.CodeValidator.Exceptions;
using LibLSLCC.CodeValidator.Primitives;
using LibLSLCC.CodeValidator.ValidatorNodes.Interfaces;
using LibLSLCC.CodeValidator.ValidatorNodes.ScopeNodes;
using LibLSLCC.Compilers;
using LSLCCGuiTestRig;
using Microsoft.Win32;

namespace LSLCCEditor
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string DefaultProgram =
            @"
default{

    state_entry(){

        llSay(0, ""Hello World!"");
    }

}
";

        private readonly LSLCustomValidatorServiceProvider _validatorServices;
        private string _currentlyOpenFile;


        private bool _pendingChanges;

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            string details = "";
            Exception i = e.Exception;

            while (i != null)
            {
                details += i.Message + "\n\n";
                i = i.InnerException;
            }

            MessageBox.Show("An unexpected error has occurred.  The progam will need to exit.\n" +
                            "Error details:\n\n" + details,
                "Unexpected error", MessageBoxButton.OK);

            App.Current.Shutdown();
        }

        public MainWindow()
        {

            App.Current.DispatcherUnhandledException += App_DispatcherUnhandledException;

            InitializeComponent();

            _validatorServices = new LSLCustomValidatorServiceProvider
            {
                ExpressionValidator = new LSLDefaultExpressionValidator(),
                MainLibraryDataProvider = new LSLDefaultLibraryDataProvider(
                    LSLLibraryBaseData.StandardLsl,
                    LSLLibraryDataAdditions.None),
                StringLiteralPreProcessor = new LSLDefaultStringPreProcessor(),
                SyntaxErrorListener = new WindowSyntaxErrorListener(this),
                SyntaxWarningListener = new WindowSyntaxWarningListener(this)
            };

            LslEditor.SetLibraryDataProvider(_validatorServices.MainLibraryDataProvider);

            LslEditor.TextEditor.TextChanged += TextEditorOnTextChanged;

            Loaded += OnLoaded;


            Title = "LSLCC Editor - (Unsaved Untitled)";
        }



        private bool PendingChanges
        {
            get { return _pendingChanges; }
            set
            {
                if (!value && string.IsNullOrWhiteSpace(CurrentlyOpenFile))
                {
                    Title = "LSLCC Editor - (Unsaved Untitled)";
                }
                if (!value && !string.IsNullOrWhiteSpace(CurrentlyOpenFile))
                {
                    Title = "LSLCC Editor - " + CurrentlyOpenFile;
                }
                if (value && !string.IsNullOrWhiteSpace(CurrentlyOpenFile))
                {
                    Title = "LSLCC Editor - (Unsaved) " + CurrentlyOpenFile;
                }
                if (value && string.IsNullOrWhiteSpace(CurrentlyOpenFile))
                {
                    Title = "LSLCC Editor - (Unsaved Untitled)";
                }
                _pendingChanges = value;
            }
        }


        private string CurrentlyOpenFile
        {
            get { return _currentlyOpenFile; }
            set
            {
                Title = "LSLCC Editor - " + value;
                _currentlyOpenFile = value;
            }
        }



        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            LslEditor.TextEditor.Text = DefaultProgram;
        }



        private void TextEditorOnTextChanged(object sender, EventArgs eventArgs)
        {
            PendingChanges = true;
        }



        private void CompilerMessageItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var message = (CompilerMessage) ((ListViewItem) sender).Content;


            

            var location = message.CodeLocation;
            int line;
            int lineend;
            if (location.LineStart == 0)
            {
                line = 1;
            }
            else
            {
                line = location.LineStart;
            }

            if (location.LineEnd == 0)
            {
                lineend = 1;
            }
            else
            {
                lineend = location.LineEnd;
            }

            




            LslEditor.TextEditor.ScrollToLine(line);


            if (message.CodeLocation.HasIndexInfo && message.CodeLocation.IsSingleLine)
            {
                LslEditor.TextEditor.Select(message.CodeLocation.StartIndex, 
                    (message.CodeLocation.StopIndex+1) - message.CodeLocation.StartIndex);

                return;

            }



            int l = 0;
            for(int i=line;i<=lineend;i++)
            {
                l += LslEditor.TextEditor.Document.GetLineByNumber(i).TotalLength;
            }

            var linestart = LslEditor.TextEditor.Document.GetLineByNumber(line);

            if (l == 0)
            {
                l = 1;
            }

            LslEditor.TextEditor.Select(linestart.Offset, l);

        }



        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenFileDialog();
            openDialog.Multiselect = false;
            openDialog.Filter = "LSL Scripts (*.lsl *.txt)|*.lsl;*.txt";
            try
            {
                if (openDialog.ShowDialog().Value)
                {
                    LslEditor.TextEditor.Load(openDialog.FileName);
                    CurrentlyOpenFile = openDialog.FileName;
                    PendingChanges = false;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Could Not Open");
                throw;
            }
        }



        private void SaveFileAs_Click(object sender, RoutedEventArgs e)
        {
            var saveDialog = new SaveFileDialog();
            saveDialog.FileName = "LSLScript.lsl";
            saveDialog.DefaultExt = ".lsl";
            saveDialog.Filter = "LSL Script (*.lsl *.txt)|*.lsl;*.txt";

            try
            {
                if (saveDialog.ShowDialog().Value)
                {
                    LslEditor.TextEditor.Save(saveDialog.FileName);
                    CurrentlyOpenFile = saveDialog.FileName;
                    PendingChanges = false;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Could Not Save");
                throw;
            }
        }



        private ILSLCompilationUnitNode ValidateCurrentEditorText()
        {
            var validator = new LSLCodeValidator(_validatorServices);
            ILSLCompilationUnitNode validated = null;


            var memoryStream = new MemoryStream(Encoding.Default.GetBytes(LslEditor.TextEditor.Text));


            try
            {
                try
                {
                    using (var infile = new StreamReader(memoryStream))
                    {
                        validated = validator.Validate(infile);
                    }
                }
                catch (LSLCodeValidatorInternalError error)
                {
                    MessageBox.Show("Please report this message with the code that caused it: " + error.Message,
                        "Internal Validation Error");
                    validated = null;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Please report this message with the code that caused it: " + error.Message,
                    "Unknown Validation Error");
                validated = null;
            }
            finally
            {
                memoryStream.Close();
                memoryStream.Dispose();
            }
            return validated;
        }



        private void CompileCurrentEditorText(string destinationFile)
        {
            CompilerMessages.Items.Clear();
            CompilerMessages.UpdateLayout();

            var validated = ValidateCurrentEditorText();

            if (validated == null)
            {
                return;
            }


            if (File.Exists(destinationFile))
            {
                File.Delete(destinationFile);
            }
            using (var outfile = File.OpenWrite(destinationFile))
            {
                var compiler = new LSLOpenSimCSCompiler(LSLOpenSimCSCompilerSettings
                    .OpenSimClientUploadable(_validatorServices.MainLibraryDataProvider));
                try
                {
                    try
                    {
                        compiler.Compile(validated, new StreamWriter(outfile, Encoding.UTF8));
                    }
                    catch (LSLCompilerInternalException error)
                    {
                        MessageBox.Show("Please report this message with the code that caused it: " + error.Message,
                            "Internal Compiler Error");
                        throw;
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Please report this message with the code that caused it: " + error.Message,
                        "Unknown Compiler Error");
                    throw;
                }
            }

            CompilerMessages.Items.Add(new CompilerMessage("Notice", "Program compiled successfully"));
            CompilerMessages.UpdateLayout();
        }



        private void Compile_Click(object sender, RoutedEventArgs e)
        {
            var suggestedFileName = "LSLScript.cs";


            if (!string.IsNullOrWhiteSpace(_currentlyOpenFile))
            {
                suggestedFileName = _currentlyOpenFile + ".cs";
            }


            var saveDialog = new SaveFileDialog();

            saveDialog.FileName = suggestedFileName;
            saveDialog.DefaultExt = ".cs";
            saveDialog.Filter = "CSharp Code (*.cs) | *.cs";


            try
            {
                if (saveDialog.ShowDialog().Value)
                {
                    SaveToCurrentFile(false);
                    CompileCurrentEditorText(saveDialog.FileName);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Could Not Compile");
                throw;
            }
        }



        private bool SaveToCurrentFile(bool requestNewFileIfNoneCurrent)
        {
            if (CurrentlyOpenFile != null)
            {
                try
                {
                    LslEditor.TextEditor.Save(CurrentlyOpenFile);
                    PendingChanges = false;
                    return true;
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Could Not Save");
                    throw;
                }
            }


            if (!requestNewFileIfNoneCurrent) return false;


            var saveDialog = new SaveFileDialog();
            saveDialog.FileName = "LSLScript.lsl";
            saveDialog.DefaultExt = ".lsl";
            saveDialog.Filter = "LSL Script (*.lsl *.txt)|*.lsl;*.txt";

            try
            {
                if (saveDialog.ShowDialog().Value)
                {
                    LslEditor.TextEditor.Save(saveDialog.FileName);
                    CurrentlyOpenFile = saveDialog.FileName;
                    PendingChanges = false;
                    return true;
                }
                return false;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Could Not Save");
                throw;
            }
        }



        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveToCurrentFile(true);
            }
            catch
            {
            }
        }



        private void ClearMessages_Click(object sender, RoutedEventArgs e)
        {
            CompilerMessages.Items.Clear();
        }



        private void CheckSyntax_Click(object sender, RoutedEventArgs e)
        {
            CompilerMessages.Items.Clear();

            var validated = ValidateCurrentEditorText();


            if (validated != null)
            {
                CompilerMessages.Items.Add(new CompilerMessage("Notice", "No Syntax errors detected."));
            }
        }



        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            if (PendingChanges)
            {
                var x = MessageBox.Show("Would you like to save before closing?", "File Unsaved",
                    MessageBoxButton.YesNoCancel);
                if (x == MessageBoxResult.Yes)
                {
                    try
                    {
                        e.Cancel = !SaveToCurrentFile(true);
                    }
                    catch
                    {
                        e.Cancel = true;
                    }
                }
                if (x == MessageBoxResult.No)
                {
                    e.Cancel = false;
                }
                if (x == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }



        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            if (PendingChanges)
            {
                var x = MessageBox.Show("Would you like to save the current file before opening a new one?",
                    "File Unsaved",
                    MessageBoxButton.YesNo);
                if (x == MessageBoxResult.Yes)
                {
                    try
                    {
                        if (SaveToCurrentFile(true))
                        {
                            LslEditor.TextEditor.Text = DefaultProgram;
                            CurrentlyOpenFile = null;
                            PendingChanges = false;
                        }
                    }
                    catch
                    {
                    }
                }
                if (x == MessageBoxResult.No)
                {
                    LslEditor.TextEditor.Text = DefaultProgram;
                    CurrentlyOpenFile = null;
                    PendingChanges = false;
                }
            }
        }



        private class WindowSyntaxErrorListener : LSLDefaultSyntaxErrorListener
        {
            private readonly MainWindow _window;



            public WindowSyntaxErrorListener(MainWindow window)
            {
                _window = window;
            }



            public override void OnError(LSLSourceCodeRange location, string message)
            {
                _window.CompilerMessages.Items.Add(new CompilerMessage("Error", location,
                    message));
                _window.CompilerMessages.UpdateLayout();
            }
        }

        private class WindowSyntaxWarningListener : LSLDefaultSyntaxWarningListener
        {
            private readonly MainWindow _window;



            public WindowSyntaxWarningListener(MainWindow window)
            {
                _window = window;
            }



            public override void OnWarning(LSLSourceCodeRange location, string message)
            {
                _window.CompilerMessages.Items.Add(new CompilerMessage("Warning",
                    location, message));
                _window.CompilerMessages.UpdateLayout();
            }
        }





        LSLLibraryDataAdditions _additionalLibrarys=LSLLibraryDataAdditions.None;


        private void LindenLsl_OnChecked(object sender, RoutedEventArgs e)
        {
            if (OpenSimLsl != null && OpenSimLsl.IsChecked)
            {
                OpenSimLsl.IsChecked = false;
                _validatorServices.MainLibraryDataProvider = new LSLDefaultLibraryDataProvider(LSLLibraryBaseData.StandardLsl, _additionalLibrarys);
                LslEditor.SetLibraryDataProvider(_validatorServices.MainLibraryDataProvider);
            }
        }



        private void OpenSimLsl_OnChecked(object sender, RoutedEventArgs e)
        {
            if (LindenLsl != null && LindenLsl.IsChecked)
            {
                LindenLsl.IsChecked = false;
                _validatorServices.MainLibraryDataProvider = new LSLDefaultLibraryDataProvider(LSLLibraryBaseData.OpensimLsl, _additionalLibrarys);
                LslEditor.SetLibraryDataProvider(_validatorServices.MainLibraryDataProvider);
            }
        }




        private void LindenLsl_OnUnchecked(object sender, RoutedEventArgs e)
        {
            
            if (OpenSimLsl!=null && !OpenSimLsl.IsChecked)
            {
                LindenLsl.IsChecked = true;
            }
        }



        private void OpenSimLsl_OnUnchecked(object sender, RoutedEventArgs e)
        {
            if (LindenLsl!=null && !LindenLsl.IsChecked)
            {
                OpenSimLsl.IsChecked = true;
            }
        }



        private void UpdateFromAdditionalLibrarys()
        {
            if (OpenSimLsl!=null && OpenSimLsl.IsChecked)
            {
                _validatorServices.MainLibraryDataProvider = new LSLDefaultLibraryDataProvider(LSLLibraryBaseData.OpensimLsl, _additionalLibrarys);
                LslEditor.SetLibraryDataProvider(_validatorServices.MainLibraryDataProvider);
            }
            if (LindenLsl!=null && LindenLsl.IsChecked)
            {
                _validatorServices.MainLibraryDataProvider = new LSLDefaultLibraryDataProvider(LSLLibraryBaseData.StandardLsl,_additionalLibrarys);
                LslEditor.SetLibraryDataProvider(_validatorServices.MainLibraryDataProvider);
            }
        }


        private void OsslFunctions_OnChecked(object sender, RoutedEventArgs e)
        {
            _additionalLibrarys |= LSLLibraryDataAdditions.OpenSimOssl;
            UpdateFromAdditionalLibrarys();
        }


        private void OsslFunctions_OnUnchecked(object sender, RoutedEventArgs e)
        {
            _additionalLibrarys &= ~LSLLibraryDataAdditions.OpenSimOssl;
            UpdateFromAdditionalLibrarys();
        }

        private void OsWindlight_OnChecked(object sender, RoutedEventArgs e)
        {
            _additionalLibrarys |= LSLLibraryDataAdditions.OpenSimWindlight;
            UpdateFromAdditionalLibrarys();
        }

        private void OsWindlight_OnUnchecked(object sender, RoutedEventArgs e)
        {
            _additionalLibrarys &= ~LSLLibraryDataAdditions.OpenSimWindlight;
            UpdateFromAdditionalLibrarys();
        }

        private void OsBulletPhysics_OnChecked(object sender, RoutedEventArgs e)
        {
            _additionalLibrarys |= LSLLibraryDataAdditions.OpenSimBulletPhysics;
            UpdateFromAdditionalLibrarys();
        }

        private void OsBulletPhysics_OnUnchecked(object sender, RoutedEventArgs e)
        {
            _additionalLibrarys &= ~LSLLibraryDataAdditions.OpenSimBulletPhysics;
            UpdateFromAdditionalLibrarys();
        }

        private void OsModInvoke_OnChecked(object sender, RoutedEventArgs e)
        {
            _additionalLibrarys |= LSLLibraryDataAdditions.OpenSimModInvoke;
            UpdateFromAdditionalLibrarys();
        }


        private void OsModInvoke_OnUnChecked(object sender, RoutedEventArgs e)
        {
            _additionalLibrarys &= ~LSLLibraryDataAdditions.OpenSimModInvoke;
            UpdateFromAdditionalLibrarys();
        }
    }

    internal class CompilerMessage
    {
        private string _messageTypeText;



        public CompilerMessage(string messageTypeText, LSLSourceCodeRange codeLocation, string messageText)
        {
            MessageTypeText = messageTypeText;
            CodeLocation = codeLocation;
            Line = codeLocation.LineStart;
            Column = codeLocation.ColumnStart;
            MessageText = messageText;
        }

        public CompilerMessage(string messageTypeText, string messageText)
        {
            MessageTypeText = messageTypeText;
            Line = 0;
            Column = 0;
            MessageText = messageText;
            CodeLocation=new LSLSourceCodeRange();
        }



        public Brush MessageTypeColor
        {
            get
            {
                if (_messageTypeText.ToLower() == "error")
                {
                    return Brushes.Crimson;
                }
                if (_messageTypeText.ToLower() == "warning")
                {
                    return Brushes.DarkGoldenrod;
                }
                return Brushes.DarkBlue;
            }
        }

        public Brush BackgroundColor
        {
            get
            {
                if (_messageTypeText.ToLower() == "error")
                {
                    return Brushes.MistyRose;
                }
                if (_messageTypeText.ToLower() == "warning")
                {
                    return Brushes.LightGoldenrodYellow;
                }
                return Brushes.AliceBlue;
            }
        }

        public string MessageTypeText
        {
            get { return _messageTypeText; }
            private set { _messageTypeText = value + ""; }
        }

        public LSLSourceCodeRange CodeLocation { get; private set; }

        public int Line { get; private set; }
        public int Column { get; private set; }

        public string LineText
        {
            get { return "(" + Line + ", " + Column + "):"; }
        }

        public string MessageText { get; private set; }
    }
}