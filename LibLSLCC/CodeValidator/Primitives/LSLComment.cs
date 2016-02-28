﻿#region FileInfo

// 
// File: LSLComment.cs
// 
// 
// ============================================================
// ============================================================
// 
// 
// Copyright (c) 2015, Eric A. Blundell
// 
// All rights reserved.
// 
// 
// This file is part of LibLSLCC.
// 
// LibLSLCC is distributed under the following BSD 3-Clause License
// 
// 
// Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
// 
// 1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
// 
// 2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer
//     in the documentation and/or other materials provided with the distribution.
// 
// 3. Neither the name of the copyright holder nor the names of its contributors may be used to endorse or promote products derived
//     from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
// HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
// LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
// ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
// OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// 
// 
// ============================================================
// ============================================================
// 
// 

#endregion

#region Imports

#endregion

namespace LibLSLCC.CodeValidator
{
    /// <summary>
    ///     A container for LSL source code comment strings.
    /// </summary>
    public struct LSLComment
    {
        private readonly string _text;
        private readonly LSLSourceCodeRange _sourceRange;
        private readonly LSLCommentType _type;


        /// <summary>
        ///     Construct a comment object from comment text, type and source code range.
        /// </summary>
        /// <param name="text">The text that up the entire comment, including the special comment start/end sequences.</param>
        /// <param name="type">The comment type.  <see cref="LSLCommentType" /></param>
        /// <param name="sourceRange">The source code range that the comment occupies.</param>
        public LSLComment(string text, LSLCommentType type, LSLSourceCodeRange sourceRange)
        {
            _text = text;
            _type = type;
            _sourceRange = sourceRange;
        }


        /// <summary>
        ///     The raw comment text.
        /// </summary>
        public string Text
        {
            get { return _text; }
        }

        /// <summary>
        ///     The source code range which the comment occupies.
        /// </summary>
        public LSLSourceCodeRange SourceRange
        {
            get { return _sourceRange; }
        }

        /// <summary>
        ///     The LSLCommentType type of the comment.
        /// </summary>
        public LSLCommentType Type
        {
            get { return _type; }
        }


        /// <summary>
        ///     Returns the comment text.
        /// </summary>
        /// <seealso cref="Text" />
        /// <returns>
        ///     <see cref="Text" />
        /// </returns>
        public override string ToString()
        {
            return Text;
        }
    }
}