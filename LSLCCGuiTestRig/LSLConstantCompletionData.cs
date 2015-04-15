using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;

namespace LSLCCEditor
{
    public class LSLConstantCompletionData : ICompletionData
    {
        private readonly string _description;
        private readonly double _priority;
        private readonly string _text;


        public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
        {
            
            textArea.Document.Replace(completionSegment, this.Text);
        }

        public LSLConstantCompletionData(string text, string description, double priority)
        {

            _description = description;
            _priority = priority;
            _text = text;
        }

        public ImageSource Image
        {
            get { return null; }
        }

        public string Text { get { return _text.Substring(1);  } }

        public object Content
        {
            get
            {
                var x= new TextBlock();
                x.Text = _text;
                x.TextAlignment=TextAlignment.Left;
                x.TextTrimming=TextTrimming.CharacterEllipsis;
                x.Foreground=new SolidColorBrush(Color.FromRgb(50, 52, 138));
                x.FontWeight = FontWeights.Bold;
                return x;
            }
        }

        public object Description
        {
            get { return _description; }
        }

        public double Priority
        {
            get { return _priority; }
        }
    }
}