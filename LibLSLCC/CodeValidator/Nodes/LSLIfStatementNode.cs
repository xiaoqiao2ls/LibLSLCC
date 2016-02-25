#region FileInfo
// 
// File: LSLIfStatementNode.cs
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

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using LibLSLCC.CodeValidator.Nodes.Interfaces;
using LibLSLCC.CodeValidator.Primitives;
using LibLSLCC.CodeValidator.Visitor;
using LibLSLCC.Parser;

#endregion

namespace LibLSLCC.CodeValidator.Nodes
{
    /// <summary>
    /// Default <see cref="ILSLIfStatementNode"/> implementation used by <see cref="LSLCodeValidator"/>
    /// </summary>
    public sealed class LSLIfStatementNode : ILSLIfStatementNode, ILSLBranchStatementNode
    {
// ReSharper disable UnusedParameter.Local
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "err")]
        private LSLIfStatementNode(LSLSourceCodeRange sourceRange, Err err)
// ReSharper restore UnusedParameter.Local
        {
            SourceRange = sourceRange;
            HasErrors = true;
        }

        /// <exception cref="ArgumentNullException"><paramref name="context"/> or <paramref name="code"/> or <paramref name="conditionExpression"/> is <c>null</c>.</exception>
        internal LSLIfStatementNode(LSLParser.ControlStructureContext context, LSLCodeScopeNode code,
            ILSLExprNode conditionExpression)
        {

            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (code == null)
            {
                throw new ArgumentNullException("code");
            }

            if (conditionExpression == null)
            {
                throw new ArgumentNullException("conditionExpression");
            }

            Code = code;
            Code.Parent = this;

            ConditionExpression = conditionExpression;
            ConditionExpression.Parent = this;

            SourceRangeIfKeyword = new LSLSourceCodeRange(context.if_keyword);
            SourceRangeOpenParenth = new LSLSourceCodeRange(context.open_parenth);
            SourceRangeCloseParenth = new LSLSourceCodeRange(context.close_parenth);

            SourceRange = new LSLSourceCodeRange(SourceRangeIfKeyword, code.SourceRange);

            SourceRangesAvailable = true;
        }


        /// <summary>
        /// <see cref="LSLCodeScopeNode.ConstantJumps"/> returned from <see cref="Code"/>
        /// </summary>
        public IEnumerable<LSLConstantJumpDescription> ConstantJumps
        {
            get { return Code == null ? new List<LSLConstantJumpDescription>() : Code.ConstantJumps; }
        }

        /// <summary>
        /// The code inside the if statement.
        /// </summary>
        public LSLCodeScopeNode Code { get; private set; }


        /// <summary>
        /// The expression inside the condition clause of the if statement.
        /// </summary>
        public ILSLExprNode ConditionExpression { get; private set; }


        ILSLReadOnlySyntaxTreeNode ILSLReadOnlySyntaxTreeNode.Parent
        {
            get { return Parent; }
        }

        ILSLCodeScopeNode ILSLIfStatementNode.Code
        {
            get { return Code; }
        }

        ILSLReadOnlyExprNode ILSLIfStatementNode.ConditionExpression
        {
            get { return ConditionExpression; }
        }

        #region ILSLBranchStatementNode Members

        /// <summary>
        /// Determines if the condition controlling the branch is a constant expression.
        /// </summary>
        public bool IsConstantBranch
        {
            get { return ConditionExpression != null && ConditionExpression.IsConstant; }
        }

        #endregion

        #region ILSLReturnPathNode Members

        /// <summary>
        /// True if the node represents a return path out of its ILSLCodeScopeNode parent, False otherwise.
        /// </summary>
        public bool HasReturnPath
        {
            get { return Code != null && Code.HasReturnPath; }
        }

        #endregion

        /// <summary>
        /// The source code range of the 'if' keyword of the statement.
        /// </summary>
        /// <remarks>If <see cref="ILSLReadOnlySyntaxTreeNode.SourceRangesAvailable"/> is <c>false</c> this property will be <c>null</c>.</remarks>
        public LSLSourceCodeRange SourceRangeIfKeyword { get; private set; }


        /// <summary>
        /// The source code range of the opening parenthesis of the condition area.
        /// </summary>
        /// <remarks>If <see cref="ILSLReadOnlySyntaxTreeNode.SourceRangesAvailable"/> is <c>false</c> this property will be <c>null</c>.</remarks>
        public LSLSourceCodeRange SourceRangeOpenParenth { get; private set; }


        /// <summary>
        /// The source code range of the closing parenthesis of the condition area.
        /// </summary>
        /// <remarks>If <see cref="ILSLReadOnlySyntaxTreeNode.SourceRangesAvailable"/> is <c>false</c> this property will be <c>null</c>.</remarks>
        public LSLSourceCodeRange SourceRangeCloseParenth { get; private set; }


        /// <summary>
        /// Returns a version of this node type that represents its error state;  in case of a syntax error
        /// in the node that prevents the node from being even partially built.
        /// </summary>
        /// <param name="sourceRange">The source code range of the error.</param>
        /// <returns>A version of this node type in its undefined/error state.</returns>
        public static
            LSLIfStatementNode GetError(LSLSourceCodeRange sourceRange)
        {
            return new LSLIfStatementNode(sourceRange, Err.Err);
        }

        #region Nested type: Err

        private enum Err
        {
            Err
        }

        #endregion

        #region ILSLTreeNode Members

        /// <summary>
        /// True if this syntax tree node contains syntax errors.
        /// </summary>
        public bool HasErrors { get; internal set; }


        /// <summary>
        /// The source code range that this syntax tree node occupies.
        /// </summary>
        /// <remarks>If <see cref="ILSLReadOnlySyntaxTreeNode.SourceRangesAvailable"/> is <c>false</c> this property will be <c>null</c>.</remarks>
        public LSLSourceCodeRange SourceRange { get; private set; }

        /// <summary>
        /// Should return true if source code ranges are available/set to meaningful values for this node.
        /// </summary>
        public bool SourceRangesAvailable { get; private set; }


        /// <summary>
        /// Accept a visit from an implementor of <see cref="ILSLValidatorNodeVisitor{T}"/>
        /// </summary>
        /// <typeparam name="T">The visitors return type.</typeparam>
        /// <param name="visitor">The visitor instance.</param>
        /// <returns>The value returned from this method in the visitor used to visit this node.</returns>
        public T AcceptVisitor<T>(ILSLValidatorNodeVisitor<T> visitor)
        {
            return visitor.VisitIfStatement(this);
        }


        /// <summary>
        /// The parent node of this syntax tree node.
        /// </summary>
        public ILSLSyntaxTreeNode Parent { get; set; }

        #endregion
    }
}