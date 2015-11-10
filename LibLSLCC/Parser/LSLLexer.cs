//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.5.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from Parser\LSL.g4 by ANTLR 4.5.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591

namespace LibLSLCC.Parser {

	using LibLSLCC.Collections;
	using LibLSLCC.CodeValidator.Enums;
	using LibLSLCC.CodeValidator.Primitives;

using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.5.1")]
[System.CLSCompliant(false)]
public partial class LSLLexer : Lexer {
	public const int
		TYPE=1, DO=2, IF=3, ELSE=4, WHILE=5, FOR=6, DEFAULT=7, STATE=8, RETURN=9, 
		JUMP=10, ID=11, HEX_LITERAL=12, INT=13, FLOAT=14, QUOTED_STRING=15, SEMI_COLON=16, 
		EQUAL=17, LOGICAL_EQUAL=18, LOGICAL_NOT_EQUAL=19, LESS_THAN=20, GREATER_THAN=21, 
		LESS_THAN_EQUAL=22, GREATER_THAN_EQUAL=23, RIGHT_SHIFT=24, LEFT_SHIFT=25, 
		RIGHT_SHIFT_EQUAL=26, LEFT_SHIFT_EQUAL=27, MINUS=28, PLUS=29, MINUS_EQUAL=30, 
		PLUS_EQUAL=31, INCREMENT=32, DECREMENT=33, MUL=34, DIV=35, MOD=36, MUL_EQUAL=37, 
		DIV_EQUAL=38, MOD_EQUAL=39, COMMA=40, O_PAREN=41, C_PAREN=42, O_BRACE=43, 
		C_BRACE=44, O_BRACKET=45, C_BRACKET=46, LABEL_PREFIX=47, BITWISE_OR=48, 
		BITWISE_AND=49, BITWISE_NOT=50, BITWISE_XOR=51, LOGICAL_NOT=52, LOGICAL_AND=53, 
		LOGICAL_OR=54, DOT=55, Whitespace=56, Newline=57, BlockComment=58, LineComment=59;
	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"TYPE", "DO", "IF", "ELSE", "WHILE", "FOR", "DEFAULT", "STATE", "RETURN", 
		"JUMP", "NameChar", "NameStartChar", "ID", "HEX_LITERAL", "INT", "FLOAT", 
		"QUOTED_STRING", "SEMI_COLON", "EQUAL", "LOGICAL_EQUAL", "LOGICAL_NOT_EQUAL", 
		"LESS_THAN", "GREATER_THAN", "LESS_THAN_EQUAL", "GREATER_THAN_EQUAL", 
		"RIGHT_SHIFT", "LEFT_SHIFT", "RIGHT_SHIFT_EQUAL", "LEFT_SHIFT_EQUAL", 
		"MINUS", "PLUS", "MINUS_EQUAL", "PLUS_EQUAL", "INCREMENT", "DECREMENT", 
		"MUL", "DIV", "MOD", "MUL_EQUAL", "DIV_EQUAL", "MOD_EQUAL", "COMMA", "O_PAREN", 
		"C_PAREN", "O_BRACE", "C_BRACE", "O_BRACKET", "C_BRACKET", "LABEL_PREFIX", 
		"BITWISE_OR", "BITWISE_AND", "BITWISE_NOT", "BITWISE_XOR", "LOGICAL_NOT", 
		"LOGICAL_AND", "LOGICAL_OR", "DOT", "Whitespace", "Newline", "BlockComment", 
		"LineComment"
	};



		public GenericArray<LSLComment> _comments = new GenericArray<LSLComment>();

		public IReadOnlyGenericArray<LSLComment> Comments { get {return _comments; } }



	    private struct LineCount
	    {
	        public int Lines;
	        public int EndColumn;
	    }
	    private static LineCount CountStringLines(int startColumn, string str)
	    {
			int cnt=0;
	        int lastLineStart = 0;
	        int i = 0;
	        int endColumn;
			foreach(var c in str){
				if(c == '\n'){
					cnt++;
	                lastLineStart = i+1;
				}
	            i++;
			}

	        if (lastLineStart == 0)
	        {
	            endColumn = (startColumn + str.Length) - 1;
	        }
	        else
	        {
	            endColumn = (i - lastLineStart) - 1;
	        }
	        return new LineCount { Lines = cnt, EndColumn = endColumn };
		}

	    /// <summary>
		/// This should be updated when the ID token rule in LSL.g4 is changed, so that the rule and this regex match
		/// This regex matches/validates LSL ID Tokens, IE: variable names, state names, label names, function names
		/// </summary>
		internal static string IDRegex = "(?:[_A-Za-z\u00C0-\u00D6\u00D8-\u00F6\u00F8-\u02FF\u0370-\u037D\u037F-\u1FFF\u200C-\u200D\u2070-\u218F\u2C00-\u2FEF\u3001-\uD7FF\uF900-\uFDCF\uFDF0-\uFFFD])(?:[_0-9A-Za-z\u00C0-\u00D6\u00D8-\u00F6\u00F8-\u02FF\u0370-\u037D\u037F-\u1FFF\u200C-\u200D\u2070-\u218F\u2C00-\u2FEF\u3001-\uD7FF\uF900-\uFDCF\uFDF0-\uFFFD\u00B7\u0300-\u036F\u203F-\u2040])*";
		

		/// <summary>
		/// This should be updated when the ID token rule in LSL.g4 is changed, so that the rule and this regex match
		/// This regex matches/validates that a character is a valid starting character for an ID Token
		/// </summary>
		internal static string IDStartCharRegex = "(?:[_A-Za-z\u00C0-\u00D6\u00D8-\u00F6\u00F8-\u02FF\u0370-\u037D\u037F-\u1FFF\u200C-\u200D\u2070-\u218F\u2C00-\u2FEF\u3001-\uD7FF\uF900-\uFDCF\uFDF0-\uFFFD])";

		/// <summary>
		/// This should be updated when the ID token rule in LSL.g4 is changed, so that the rule and this regex match
		/// This regex matches/validates that a character is a valid trailing character after the first character of an ID Token
		/// </summary>
		internal static string IDTrailingCharRegex = "(?:[_0-9A-Za-z\u00C0-\u00D6\u00D8-\u00F6\u00F8-\u02FF\u0370-\u037D\u037F-\u1FFF\u200C-\u200D\u2070-\u218F\u2C00-\u2FEF\u3001-\uD7FF\uF900-\uFDCF\uFDF0-\uFFFD\u00B7\u0300-\u036F\u203F-\u2040])";


	public LSLLexer(ICharStream input)
		: base(input)
	{
		Interpreter = new LexerATNSimulator(this,_ATN);
	}

	private static readonly string[] _LiteralNames = {
		null, null, "'do'", "'if'", "'else'", "'while'", "'for'", "'default'", 
		"'state'", "'return'", "'jump'", null, null, null, null, null, "';'", 
		"'='", "'=='", "'!='", "'<'", "'>'", "'<='", "'>='", "'>>'", "'<<'", "'>>='", 
		"'<<='", "'-'", "'+'", "'-='", "'+='", "'++'", "'--'", "'*'", "'/'", "'%'", 
		"'*='", "'/='", "'%='", "','", "'('", "')'", "'{'", "'}'", "'['", "']'", 
		"'@'", "'|'", "'&'", "'~'", "'^'", "'!'", "'&&'", "'||'", "'.'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "TYPE", "DO", "IF", "ELSE", "WHILE", "FOR", "DEFAULT", "STATE", 
		"RETURN", "JUMP", "ID", "HEX_LITERAL", "INT", "FLOAT", "QUOTED_STRING", 
		"SEMI_COLON", "EQUAL", "LOGICAL_EQUAL", "LOGICAL_NOT_EQUAL", "LESS_THAN", 
		"GREATER_THAN", "LESS_THAN_EQUAL", "GREATER_THAN_EQUAL", "RIGHT_SHIFT", 
		"LEFT_SHIFT", "RIGHT_SHIFT_EQUAL", "LEFT_SHIFT_EQUAL", "MINUS", "PLUS", 
		"MINUS_EQUAL", "PLUS_EQUAL", "INCREMENT", "DECREMENT", "MUL", "DIV", "MOD", 
		"MUL_EQUAL", "DIV_EQUAL", "MOD_EQUAL", "COMMA", "O_PAREN", "C_PAREN", 
		"O_BRACE", "C_BRACE", "O_BRACKET", "C_BRACKET", "LABEL_PREFIX", "BITWISE_OR", 
		"BITWISE_AND", "BITWISE_NOT", "BITWISE_XOR", "LOGICAL_NOT", "LOGICAL_AND", 
		"LOGICAL_OR", "DOT", "Whitespace", "Newline", "BlockComment", "LineComment"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "LSL.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }

	public override void Action(RuleContext _localctx, int ruleIndex, int actionIndex) {
		switch (ruleIndex) {
		case 59 : BlockComment_action(_localctx, actionIndex); break;
		case 60 : LineComment_action(_localctx, actionIndex); break;
		}
	}
	private void BlockComment_action(RuleContext _localctx, int actionIndex) {
		switch (actionIndex) {
		case 0: 
		                    var lineData = CountStringLines(this.TokenStartColumn, this.Text);
							_comments.Add(new LSLComment()
							{
								Text = this.Text, 
		                        SourceCodeRange = new LSLSourceCodeRange(
		                            this.TokenStartLine,
		                            this.TokenStartColumn,
		                            this.TokenStartLine + lineData.Lines, 
		                            lineData.EndColumn, 
		                            this.TokenStartCharIndex, 
		                            this.Text.Length+this.TokenStartCharIndex),
									Type = LSLCommentType.Block
							});
						 break;
		}
	}
	private void LineComment_action(RuleContext _localctx, int actionIndex) {
		switch (actionIndex) {
		case 1: 
		                    var lineData = CountStringLines(this.TokenStartColumn, this.Text);
							_comments.Add(new LSLComment()
							{
								Text = this.Text, 
		                        SourceCodeRange = new LSLSourceCodeRange(
		                            this.TokenStartLine,
		                            this.TokenStartColumn,
		                            this.TokenStartLine + lineData.Lines, 
		                            lineData.EndColumn, 
		                            this.TokenStartCharIndex, 
		                            this.Text.Length+this.TokenStartCharIndex),
									Type = LSLCommentType.SingleLine
							});
						 break;
		}
	}

	public static readonly string _serializedATN =
		"\x3\x430\xD6D1\x8206\xAD2D\x4417\xAEF1\x8D80\xAADD\x2=\x1B0\b\x1\x4\x2"+
		"\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b\x4"+
		"\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4\x10"+
		"\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4\x15\t\x15"+
		"\x4\x16\t\x16\x4\x17\t\x17\x4\x18\t\x18\x4\x19\t\x19\x4\x1A\t\x1A\x4\x1B"+
		"\t\x1B\x4\x1C\t\x1C\x4\x1D\t\x1D\x4\x1E\t\x1E\x4\x1F\t\x1F\x4 \t \x4!"+
		"\t!\x4\"\t\"\x4#\t#\x4$\t$\x4%\t%\x4&\t&\x4\'\t\'\x4(\t(\x4)\t)\x4*\t"+
		"*\x4+\t+\x4,\t,\x4-\t-\x4.\t.\x4/\t/\x4\x30\t\x30\x4\x31\t\x31\x4\x32"+
		"\t\x32\x4\x33\t\x33\x4\x34\t\x34\x4\x35\t\x35\x4\x36\t\x36\x4\x37\t\x37"+
		"\x4\x38\t\x38\x4\x39\t\x39\x4:\t:\x4;\t;\x4<\t<\x4=\t=\x4>\t>\x3\x2\x3"+
		"\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2"+
		"\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3"+
		"\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2"+
		"\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3"+
		"\x2\x3\x2\x5\x2\xAF\n\x2\x3\x3\x3\x3\x3\x3\x3\x4\x3\x4\x3\x4\x3\x5\x3"+
		"\x5\x3\x5\x3\x5\x3\x5\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3\a\x3\a\x3"+
		"\a\x3\a\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\t\x3\t\x3\t\x3\t\x3"+
		"\t\x3\t\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\v\x3\v\x3\v\x3\v\x3\v\x3"+
		"\f\x3\f\x5\f\xE2\n\f\x3\r\x3\r\x3\xE\x3\xE\a\xE\xE8\n\xE\f\xE\xE\xE\xEB"+
		"\v\xE\x3\xF\x3\xF\x3\xF\x3\xF\x6\xF\xF1\n\xF\r\xF\xE\xF\xF2\x3\x10\x6"+
		"\x10\xF6\n\x10\r\x10\xE\x10\xF7\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3"+
		"\x11\x3\x11\x3\x11\x3\x11\x5\x11\x103\n\x11\x3\x11\x3\x11\x3\x11\x5\x11"+
		"\x108\n\x11\x3\x11\x6\x11\x10B\n\x11\r\x11\xE\x11\x10C\x5\x11\x10F\n\x11"+
		"\x3\x11\x5\x11\x112\n\x11\x3\x12\x3\x12\x3\x12\x3\x12\x3\x12\x3\x12\a"+
		"\x12\x11A\n\x12\f\x12\xE\x12\x11D\v\x12\x3\x12\x3\x12\x3\x13\x3\x13\x3"+
		"\x14\x3\x14\x3\x15\x3\x15\x3\x15\x3\x16\x3\x16\x3\x16\x3\x17\x3\x17\x3"+
		"\x18\x3\x18\x3\x19\x3\x19\x3\x19\x3\x1A\x3\x1A\x3\x1A\x3\x1B\x3\x1B\x3"+
		"\x1B\x3\x1C\x3\x1C\x3\x1C\x3\x1D\x3\x1D\x3\x1D\x3\x1D\x3\x1E\x3\x1E\x3"+
		"\x1E\x3\x1E\x3\x1F\x3\x1F\x3 \x3 \x3!\x3!\x3!\x3\"\x3\"\x3\"\x3#\x3#\x3"+
		"#\x3$\x3$\x3$\x3%\x3%\x3&\x3&\x3\'\x3\'\x3(\x3(\x3(\x3)\x3)\x3)\x3*\x3"+
		"*\x3*\x3+\x3+\x3,\x3,\x3-\x3-\x3.\x3.\x3/\x3/\x3\x30\x3\x30\x3\x31\x3"+
		"\x31\x3\x32\x3\x32\x3\x33\x3\x33\x3\x34\x3\x34\x3\x35\x3\x35\x3\x36\x3"+
		"\x36\x3\x37\x3\x37\x3\x38\x3\x38\x3\x38\x3\x39\x3\x39\x3\x39\x3:\x3:\x3"+
		";\x6;\x185\n;\r;\xE;\x186\x3;\x3;\x3<\x3<\x5<\x18D\n<\x3<\x5<\x190\n<"+
		"\x3<\x3<\x3=\x3=\x3=\x3=\a=\x198\n=\f=\xE=\x19B\v=\x3=\x3=\x3=\x3=\x3"+
		"=\x3=\x3=\x3>\x3>\x3>\x3>\a>\x1A8\n>\f>\xE>\x1AB\v>\x3>\x3>\x3>\x3>\x4"+
		"\x11B\x199\x2?\x3\x3\x5\x4\a\x5\t\x6\v\a\r\b\xF\t\x11\n\x13\v\x15\f\x17"+
		"\x2\x19\x2\x1B\r\x1D\xE\x1F\xF!\x10#\x11%\x12\'\x13)\x14+\x15-\x16/\x17"+
		"\x31\x18\x33\x19\x35\x1A\x37\x1B\x39\x1C;\x1D=\x1E?\x1F\x41 \x43!\x45"+
		"\"G#I$K%M&O\'Q(S)U*W+Y,[-]._/\x61\x30\x63\x31\x65\x32g\x33i\x34k\x35m"+
		"\x36o\x37q\x38s\x39u:w;y<{=\x3\x2\v\a\x2\x32;\x61\x61\xB9\xB9\x302\x371"+
		"\x2041\x2042\x10\x2\x43\\\x61\x61\x63|\xC2\xD8\xDA\xF8\xFA\x301\x372\x37F"+
		"\x381\x2001\x200E\x200F\x2072\x2191\x2C02\x2FF1\x3003\xD801\xF902\xFDD1"+
		"\xFDF2\xFFFF\x5\x2\x32;\x43H\x63h\x3\x2\x32;\x4\x2GGgg\x4\x2HHhh\x3\x2"+
		"$$\x4\x2\v\v\"\"\x4\x2\f\f\xF\xF\x1C6\x2\x3\x3\x2\x2\x2\x2\x5\x3\x2\x2"+
		"\x2\x2\a\x3\x2\x2\x2\x2\t\x3\x2\x2\x2\x2\v\x3\x2\x2\x2\x2\r\x3\x2\x2\x2"+
		"\x2\xF\x3\x2\x2\x2\x2\x11\x3\x2\x2\x2\x2\x13\x3\x2\x2\x2\x2\x15\x3\x2"+
		"\x2\x2\x2\x1B\x3\x2\x2\x2\x2\x1D\x3\x2\x2\x2\x2\x1F\x3\x2\x2\x2\x2!\x3"+
		"\x2\x2\x2\x2#\x3\x2\x2\x2\x2%\x3\x2\x2\x2\x2\'\x3\x2\x2\x2\x2)\x3\x2\x2"+
		"\x2\x2+\x3\x2\x2\x2\x2-\x3\x2\x2\x2\x2/\x3\x2\x2\x2\x2\x31\x3\x2\x2\x2"+
		"\x2\x33\x3\x2\x2\x2\x2\x35\x3\x2\x2\x2\x2\x37\x3\x2\x2\x2\x2\x39\x3\x2"+
		"\x2\x2\x2;\x3\x2\x2\x2\x2=\x3\x2\x2\x2\x2?\x3\x2\x2\x2\x2\x41\x3\x2\x2"+
		"\x2\x2\x43\x3\x2\x2\x2\x2\x45\x3\x2\x2\x2\x2G\x3\x2\x2\x2\x2I\x3\x2\x2"+
		"\x2\x2K\x3\x2\x2\x2\x2M\x3\x2\x2\x2\x2O\x3\x2\x2\x2\x2Q\x3\x2\x2\x2\x2"+
		"S\x3\x2\x2\x2\x2U\x3\x2\x2\x2\x2W\x3\x2\x2\x2\x2Y\x3\x2\x2\x2\x2[\x3\x2"+
		"\x2\x2\x2]\x3\x2\x2\x2\x2_\x3\x2\x2\x2\x2\x61\x3\x2\x2\x2\x2\x63\x3\x2"+
		"\x2\x2\x2\x65\x3\x2\x2\x2\x2g\x3\x2\x2\x2\x2i\x3\x2\x2\x2\x2k\x3\x2\x2"+
		"\x2\x2m\x3\x2\x2\x2\x2o\x3\x2\x2\x2\x2q\x3\x2\x2\x2\x2s\x3\x2\x2\x2\x2"+
		"u\x3\x2\x2\x2\x2w\x3\x2\x2\x2\x2y\x3\x2\x2\x2\x2{\x3\x2\x2\x2\x3\xAE\x3"+
		"\x2\x2\x2\x5\xB0\x3\x2\x2\x2\a\xB3\x3\x2\x2\x2\t\xB6\x3\x2\x2\x2\v\xBB"+
		"\x3\x2\x2\x2\r\xC1\x3\x2\x2\x2\xF\xC5\x3\x2\x2\x2\x11\xCD\x3\x2\x2\x2"+
		"\x13\xD3\x3\x2\x2\x2\x15\xDA\x3\x2\x2\x2\x17\xE1\x3\x2\x2\x2\x19\xE3\x3"+
		"\x2\x2\x2\x1B\xE5\x3\x2\x2\x2\x1D\xEC\x3\x2\x2\x2\x1F\xF5\x3\x2\x2\x2"+
		"!\x102\x3\x2\x2\x2#\x113\x3\x2\x2\x2%\x120\x3\x2\x2\x2\'\x122\x3\x2\x2"+
		"\x2)\x124\x3\x2\x2\x2+\x127\x3\x2\x2\x2-\x12A\x3\x2\x2\x2/\x12C\x3\x2"+
		"\x2\x2\x31\x12E\x3\x2\x2\x2\x33\x131\x3\x2\x2\x2\x35\x134\x3\x2\x2\x2"+
		"\x37\x137\x3\x2\x2\x2\x39\x13A\x3\x2\x2\x2;\x13E\x3\x2\x2\x2=\x142\x3"+
		"\x2\x2\x2?\x144\x3\x2\x2\x2\x41\x146\x3\x2\x2\x2\x43\x149\x3\x2\x2\x2"+
		"\x45\x14C\x3\x2\x2\x2G\x14F\x3\x2\x2\x2I\x152\x3\x2\x2\x2K\x154\x3\x2"+
		"\x2\x2M\x156\x3\x2\x2\x2O\x158\x3\x2\x2\x2Q\x15B\x3\x2\x2\x2S\x15E\x3"+
		"\x2\x2\x2U\x161\x3\x2\x2\x2W\x163\x3\x2\x2\x2Y\x165\x3\x2\x2\x2[\x167"+
		"\x3\x2\x2\x2]\x169\x3\x2\x2\x2_\x16B\x3\x2\x2\x2\x61\x16D\x3\x2\x2\x2"+
		"\x63\x16F\x3\x2\x2\x2\x65\x171\x3\x2\x2\x2g\x173\x3\x2\x2\x2i\x175\x3"+
		"\x2\x2\x2k\x177\x3\x2\x2\x2m\x179\x3\x2\x2\x2o\x17B\x3\x2\x2\x2q\x17E"+
		"\x3\x2\x2\x2s\x181\x3\x2\x2\x2u\x184\x3\x2\x2\x2w\x18F\x3\x2\x2\x2y\x193"+
		"\x3\x2\x2\x2{\x1A3\x3\x2\x2\x2}~\an\x2\x2~\x7F\ak\x2\x2\x7F\x80\au\x2"+
		"\x2\x80\xAF\av\x2\x2\x81\x82\ax\x2\x2\x82\x83\ag\x2\x2\x83\x84\a\x65\x2"+
		"\x2\x84\x85\av\x2\x2\x85\x86\aq\x2\x2\x86\xAF\at\x2\x2\x87\x88\ah\x2\x2"+
		"\x88\x89\an\x2\x2\x89\x8A\aq\x2\x2\x8A\x8B\a\x63\x2\x2\x8B\xAF\av\x2\x2"+
		"\x8C\x8D\ak\x2\x2\x8D\x8E\ap\x2\x2\x8E\x8F\av\x2\x2\x8F\x90\ag\x2\x2\x90"+
		"\x91\ai\x2\x2\x91\x92\ag\x2\x2\x92\xAF\at\x2\x2\x93\x94\au\x2\x2\x94\x95"+
		"\av\x2\x2\x95\x96\at\x2\x2\x96\x97\ak\x2\x2\x97\x98\ap\x2\x2\x98\xAF\a"+
		"i\x2\x2\x99\x9A\at\x2\x2\x9A\x9B\aq\x2\x2\x9B\x9C\av\x2\x2\x9C\x9D\a\x63"+
		"\x2\x2\x9D\x9E\av\x2\x2\x9E\x9F\ak\x2\x2\x9F\xA0\aq\x2\x2\xA0\xAF\ap\x2"+
		"\x2\xA1\xA2\as\x2\x2\xA2\xA3\aw\x2\x2\xA3\xA4\a\x63\x2\x2\xA4\xA5\av\x2"+
		"\x2\xA5\xA6\ag\x2\x2\xA6\xA7\at\x2\x2\xA7\xA8\ap\x2\x2\xA8\xA9\ak\x2\x2"+
		"\xA9\xAA\aq\x2\x2\xAA\xAF\ap\x2\x2\xAB\xAC\am\x2\x2\xAC\xAD\ag\x2\x2\xAD"+
		"\xAF\a{\x2\x2\xAE}\x3\x2\x2\x2\xAE\x81\x3\x2\x2\x2\xAE\x87\x3\x2\x2\x2"+
		"\xAE\x8C\x3\x2\x2\x2\xAE\x93\x3\x2\x2\x2\xAE\x99\x3\x2\x2\x2\xAE\xA1\x3"+
		"\x2\x2\x2\xAE\xAB\x3\x2\x2\x2\xAF\x4\x3\x2\x2\x2\xB0\xB1\a\x66\x2\x2\xB1"+
		"\xB2\aq\x2\x2\xB2\x6\x3\x2\x2\x2\xB3\xB4\ak\x2\x2\xB4\xB5\ah\x2\x2\xB5"+
		"\b\x3\x2\x2\x2\xB6\xB7\ag\x2\x2\xB7\xB8\an\x2\x2\xB8\xB9\au\x2\x2\xB9"+
		"\xBA\ag\x2\x2\xBA\n\x3\x2\x2\x2\xBB\xBC\ay\x2\x2\xBC\xBD\aj\x2\x2\xBD"+
		"\xBE\ak\x2\x2\xBE\xBF\an\x2\x2\xBF\xC0\ag\x2\x2\xC0\f\x3\x2\x2\x2\xC1"+
		"\xC2\ah\x2\x2\xC2\xC3\aq\x2\x2\xC3\xC4\at\x2\x2\xC4\xE\x3\x2\x2\x2\xC5"+
		"\xC6\a\x66\x2\x2\xC6\xC7\ag\x2\x2\xC7\xC8\ah\x2\x2\xC8\xC9\a\x63\x2\x2"+
		"\xC9\xCA\aw\x2\x2\xCA\xCB\an\x2\x2\xCB\xCC\av\x2\x2\xCC\x10\x3\x2\x2\x2"+
		"\xCD\xCE\au\x2\x2\xCE\xCF\av\x2\x2\xCF\xD0\a\x63\x2\x2\xD0\xD1\av\x2\x2"+
		"\xD1\xD2\ag\x2\x2\xD2\x12\x3\x2\x2\x2\xD3\xD4\at\x2\x2\xD4\xD5\ag\x2\x2"+
		"\xD5\xD6\av\x2\x2\xD6\xD7\aw\x2\x2\xD7\xD8\at\x2\x2\xD8\xD9\ap\x2\x2\xD9"+
		"\x14\x3\x2\x2\x2\xDA\xDB\al\x2\x2\xDB\xDC\aw\x2\x2\xDC\xDD\ao\x2\x2\xDD"+
		"\xDE\ar\x2\x2\xDE\x16\x3\x2\x2\x2\xDF\xE2\x5\x19\r\x2\xE0\xE2\t\x2\x2"+
		"\x2\xE1\xDF\x3\x2\x2\x2\xE1\xE0\x3\x2\x2\x2\xE2\x18\x3\x2\x2\x2\xE3\xE4"+
		"\t\x3\x2\x2\xE4\x1A\x3\x2\x2\x2\xE5\xE9\x5\x19\r\x2\xE6\xE8\x5\x17\f\x2"+
		"\xE7\xE6\x3\x2\x2\x2\xE8\xEB\x3\x2\x2\x2\xE9\xE7\x3\x2\x2\x2\xE9\xEA\x3"+
		"\x2\x2\x2\xEA\x1C\x3\x2\x2\x2\xEB\xE9\x3\x2\x2\x2\xEC\xED\a\x32\x2\x2"+
		"\xED\xEE\az\x2\x2\xEE\xF0\x3\x2\x2\x2\xEF\xF1\t\x4\x2\x2\xF0\xEF\x3\x2"+
		"\x2\x2\xF1\xF2\x3\x2\x2\x2\xF2\xF0\x3\x2\x2\x2\xF2\xF3\x3\x2\x2\x2\xF3"+
		"\x1E\x3\x2\x2\x2\xF4\xF6\t\x5\x2\x2\xF5\xF4\x3\x2\x2\x2\xF6\xF7\x3\x2"+
		"\x2\x2\xF7\xF5\x3\x2\x2\x2\xF7\xF8\x3\x2\x2\x2\xF8 \x3\x2\x2\x2\xF9\xFA"+
		"\x5\x1F\x10\x2\xFA\xFB\a\x30\x2\x2\xFB\xFC\x5\x1F\x10\x2\xFC\x103\x3\x2"+
		"\x2\x2\xFD\xFE\a\x30\x2\x2\xFE\x103\x5\x1F\x10\x2\xFF\x100\x5\x1F\x10"+
		"\x2\x100\x101\a\x30\x2\x2\x101\x103\x3\x2\x2\x2\x102\xF9\x3\x2\x2\x2\x102"+
		"\xFD\x3\x2\x2\x2\x102\xFF\x3\x2\x2\x2\x103\x10E\x3\x2\x2\x2\x104\x107"+
		"\t\x6\x2\x2\x105\x108\x5? \x2\x106\x108\x5=\x1F\x2\x107\x105\x3\x2\x2"+
		"\x2\x107\x106\x3\x2\x2\x2\x108\x10A\x3\x2\x2\x2\x109\x10B\x5\x1F\x10\x2"+
		"\x10A\x109\x3\x2\x2\x2\x10B\x10C\x3\x2\x2\x2\x10C\x10A\x3\x2\x2\x2\x10C"+
		"\x10D\x3\x2\x2\x2\x10D\x10F\x3\x2\x2\x2\x10E\x104\x3\x2\x2\x2\x10E\x10F"+
		"\x3\x2\x2\x2\x10F\x111\x3\x2\x2\x2\x110\x112\t\a\x2\x2\x111\x110\x3\x2"+
		"\x2\x2\x111\x112\x3\x2\x2\x2\x112\"\x3\x2\x2\x2\x113\x11B\a$\x2\x2\x114"+
		"\x115\a^\x2\x2\x115\x11A\a$\x2\x2\x116\x117\a^\x2\x2\x117\x11A\a^\x2\x2"+
		"\x118\x11A\n\b\x2\x2\x119\x114\x3\x2\x2\x2\x119\x116\x3\x2\x2\x2\x119"+
		"\x118\x3\x2\x2\x2\x11A\x11D\x3\x2\x2\x2\x11B\x11C\x3\x2\x2\x2\x11B\x119"+
		"\x3\x2\x2\x2\x11C\x11E\x3\x2\x2\x2\x11D\x11B\x3\x2\x2\x2\x11E\x11F\a$"+
		"\x2\x2\x11F$\x3\x2\x2\x2\x120\x121\a=\x2\x2\x121&\x3\x2\x2\x2\x122\x123"+
		"\a?\x2\x2\x123(\x3\x2\x2\x2\x124\x125\a?\x2\x2\x125\x126\a?\x2\x2\x126"+
		"*\x3\x2\x2\x2\x127\x128\a#\x2\x2\x128\x129\a?\x2\x2\x129,\x3\x2\x2\x2"+
		"\x12A\x12B\a>\x2\x2\x12B.\x3\x2\x2\x2\x12C\x12D\a@\x2\x2\x12D\x30\x3\x2"+
		"\x2\x2\x12E\x12F\a>\x2\x2\x12F\x130\a?\x2\x2\x130\x32\x3\x2\x2\x2\x131"+
		"\x132\a@\x2\x2\x132\x133\a?\x2\x2\x133\x34\x3\x2\x2\x2\x134\x135\a@\x2"+
		"\x2\x135\x136\a@\x2\x2\x136\x36\x3\x2\x2\x2\x137\x138\a>\x2\x2\x138\x139"+
		"\a>\x2\x2\x139\x38\x3\x2\x2\x2\x13A\x13B\a@\x2\x2\x13B\x13C\a@\x2\x2\x13C"+
		"\x13D\a?\x2\x2\x13D:\x3\x2\x2\x2\x13E\x13F\a>\x2\x2\x13F\x140\a>\x2\x2"+
		"\x140\x141\a?\x2\x2\x141<\x3\x2\x2\x2\x142\x143\a/\x2\x2\x143>\x3\x2\x2"+
		"\x2\x144\x145\a-\x2\x2\x145@\x3\x2\x2\x2\x146\x147\a/\x2\x2\x147\x148"+
		"\a?\x2\x2\x148\x42\x3\x2\x2\x2\x149\x14A\a-\x2\x2\x14A\x14B\a?\x2\x2\x14B"+
		"\x44\x3\x2\x2\x2\x14C\x14D\a-\x2\x2\x14D\x14E\a-\x2\x2\x14E\x46\x3\x2"+
		"\x2\x2\x14F\x150\a/\x2\x2\x150\x151\a/\x2\x2\x151H\x3\x2\x2\x2\x152\x153"+
		"\a,\x2\x2\x153J\x3\x2\x2\x2\x154\x155\a\x31\x2\x2\x155L\x3\x2\x2\x2\x156"+
		"\x157\a\'\x2\x2\x157N\x3\x2\x2\x2\x158\x159\a,\x2\x2\x159\x15A\a?\x2\x2"+
		"\x15AP\x3\x2\x2\x2\x15B\x15C\a\x31\x2\x2\x15C\x15D\a?\x2\x2\x15DR\x3\x2"+
		"\x2\x2\x15E\x15F\a\'\x2\x2\x15F\x160\a?\x2\x2\x160T\x3\x2\x2\x2\x161\x162"+
		"\a.\x2\x2\x162V\x3\x2\x2\x2\x163\x164\a*\x2\x2\x164X\x3\x2\x2\x2\x165"+
		"\x166\a+\x2\x2\x166Z\x3\x2\x2\x2\x167\x168\a}\x2\x2\x168\\\x3\x2\x2\x2"+
		"\x169\x16A\a\x7F\x2\x2\x16A^\x3\x2\x2\x2\x16B\x16C\a]\x2\x2\x16C`\x3\x2"+
		"\x2\x2\x16D\x16E\a_\x2\x2\x16E\x62\x3\x2\x2\x2\x16F\x170\a\x42\x2\x2\x170"+
		"\x64\x3\x2\x2\x2\x171\x172\a~\x2\x2\x172\x66\x3\x2\x2\x2\x173\x174\a("+
		"\x2\x2\x174h\x3\x2\x2\x2\x175\x176\a\x80\x2\x2\x176j\x3\x2\x2\x2\x177"+
		"\x178\a`\x2\x2\x178l\x3\x2\x2\x2\x179\x17A\a#\x2\x2\x17An\x3\x2\x2\x2"+
		"\x17B\x17C\a(\x2\x2\x17C\x17D\a(\x2\x2\x17Dp\x3\x2\x2\x2\x17E\x17F\a~"+
		"\x2\x2\x17F\x180\a~\x2\x2\x180r\x3\x2\x2\x2\x181\x182\a\x30\x2\x2\x182"+
		"t\x3\x2\x2\x2\x183\x185\t\t\x2\x2\x184\x183\x3\x2\x2\x2\x185\x186\x3\x2"+
		"\x2\x2\x186\x184\x3\x2\x2\x2\x186\x187\x3\x2\x2\x2\x187\x188\x3\x2\x2"+
		"\x2\x188\x189\b;\x2\x2\x189v\x3\x2\x2\x2\x18A\x18C\a\xF\x2\x2\x18B\x18D"+
		"\a\f\x2\x2\x18C\x18B\x3\x2\x2\x2\x18C\x18D\x3\x2\x2\x2\x18D\x190\x3\x2"+
		"\x2\x2\x18E\x190\a\f\x2\x2\x18F\x18A\x3\x2\x2\x2\x18F\x18E\x3\x2\x2\x2"+
		"\x190\x191\x3\x2\x2\x2\x191\x192\b<\x2\x2\x192x\x3\x2\x2\x2\x193\x194"+
		"\a\x31\x2\x2\x194\x195\a,\x2\x2\x195\x199\x3\x2\x2\x2\x196\x198\v\x2\x2"+
		"\x2\x197\x196\x3\x2\x2\x2\x198\x19B\x3\x2\x2\x2\x199\x19A\x3\x2\x2\x2"+
		"\x199\x197\x3\x2\x2\x2\x19A\x19C\x3\x2\x2\x2\x19B\x199\x3\x2\x2\x2\x19C"+
		"\x19D\a,\x2\x2\x19D\x19E\a\x31\x2\x2\x19E\x19F\x3\x2\x2\x2\x19F\x1A0\b"+
		"=\x3\x2\x1A0\x1A1\x3\x2\x2\x2\x1A1\x1A2\b=\x4\x2\x1A2z\x3\x2\x2\x2\x1A3"+
		"\x1A4\a\x31\x2\x2\x1A4\x1A5\a\x31\x2\x2\x1A5\x1A9\x3\x2\x2\x2\x1A6\x1A8"+
		"\n\n\x2\x2\x1A7\x1A6\x3\x2\x2\x2\x1A8\x1AB\x3\x2\x2\x2\x1A9\x1A7\x3\x2"+
		"\x2\x2\x1A9\x1AA\x3\x2\x2\x2\x1AA\x1AC\x3\x2\x2\x2\x1AB\x1A9\x3\x2\x2"+
		"\x2\x1AC\x1AD\b>\x5\x2\x1AD\x1AE\x3\x2\x2\x2\x1AE\x1AF\b>\x4\x2\x1AF|"+
		"\x3\x2\x2\x2\x14\x2\xAE\xE1\xE9\xF2\xF7\x102\x107\x10C\x10E\x111\x119"+
		"\x11B\x186\x18C\x18F\x199\x1A9\x6\b\x2\x2\x3=\x2\x2\x3\x2\x3>\x3";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace LibLSLCC.Parser
