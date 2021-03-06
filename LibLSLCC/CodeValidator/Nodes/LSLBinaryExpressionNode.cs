﻿#region FileInfo

// 
// File: LSLBinaryExpressionNode.cs
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
using System.Diagnostics.CodeAnalysis;
using Antlr4.Runtime;
using LibLSLCC.AntlrParser;

#endregion

namespace LibLSLCC.CodeValidator
{
    /// <summary>
    ///     Default <see cref="ILSLBinaryExpressionNode" /> implementation used by <see cref="LSLCodeValidator" />
    /// </summary>
    public sealed class LSLBinaryExpressionNode : ILSLBinaryExpressionNode, ILSLExprNode
    {
        private ILSLSyntaxTreeNode _parent;
// ReSharper disable UnusedParameter.Local
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "err")]
        private LSLBinaryExpressionNode(LSLSourceCodeRange sourceRange, Err err)
// ReSharper restore UnusedParameter.Local
        {
            SourceRange = sourceRange;
            HasErrors = true;
        }


        /// <summary>
        ///     Create an <see cref="LSLBinaryExpressionNode" /> by cloning from another.
        /// </summary>
        /// <param name="other">The other node to clone from.</param>
        private LSLBinaryExpressionNode(LSLBinaryExpressionNode other)
        {
            Type = other.Type;
            LeftExpression = other.LeftExpression.Clone();
            RightExpression = other.RightExpression.Clone();

            LeftExpression.Parent = this;
            RightExpression.Parent = this;

            SourceRangesAvailable = other.SourceRangesAvailable;

            Operation = other.Operation;
            OperationString = other.OperationString;


            if (SourceRangesAvailable)
            {
                SourceRange = other.SourceRange;

                SourceRangeOperation = other.SourceRangeOperation;
            }

            HasErrors = other.HasErrors;
        }


        /// <summary>
        ///     Create a <see cref="LSLBinaryExpressionNode" /> from two <see cref="ILSLExprNode" />'s and an operator description.
        /// </summary>
        /// <param name="resultType">
        ///     The resulting type of the binary operation between <paramref name="leftExpression" /> and
        ///     <paramref name="rightExpression" />.
        /// </param>
        /// <param name="leftExpression">The left expression.</param>
        /// <param name="operation">The operator.</param>
        /// <param name="rightExpression">The right expression.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="leftExpression" /> or <paramref name="rightExpression" /> is
        ///     <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     Thrown if <paramref name="resultType" /> is equal to <see cref="LSLType.Void" /> or
        ///     <paramref name="operation" /> is equal to <see cref="LSLBinaryOperationType.Error" />
        /// </exception>
        public LSLBinaryExpressionNode(LSLType resultType, ILSLExprNode leftExpression, LSLBinaryOperationType operation,
            ILSLExprNode rightExpression)
        {
            if (leftExpression == null)
            {
                throw new ArgumentNullException("leftExpression");
            }
            if (rightExpression == null)
            {
                throw new ArgumentNullException("rightExpression");
            }
            if (resultType == LSLType.Void)
            {
                throw new ArgumentException("Binary operation resultType cannot be LSLType.Void.", "resultType");
            }


            Type = resultType;

            LeftExpression = leftExpression;
            LeftExpression.Parent = this;

            Operation = operation;
            OperationString = operation.ToOperatorString();

            RightExpression = rightExpression;
            RightExpression.Parent = this;
        }


        /// <exception cref="ArgumentNullException">
        ///     <paramref name="context" /> or
        ///     <paramref name="leftExpression" /> or
        ///     <paramref name="rightExpression" /> is <c>null</c>.
        /// </exception>
        internal LSLBinaryExpressionNode(
            LSLParser.ExpressionContext context,
            IToken operationToken,
            ILSLExprNode leftExpression,
            ILSLExprNode rightExpression,
            LSLType returns,
            string operationString)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (leftExpression == null)
            {
                throw new ArgumentNullException("leftExpression");
            }

            if (rightExpression == null)
            {
                throw new ArgumentNullException("rightExpression");
            }


            Type = returns;
            LeftExpression = leftExpression;
            RightExpression = rightExpression;

            leftExpression.Parent = this;
            rightExpression.Parent = this;

            ParseAndSetOperation(operationString);

            SourceRange = new LSLSourceCodeRange(context);

            SourceRangeOperation = new LSLSourceCodeRange(operationToken);

            SourceRangesAvailable = true;
        }


        /// <summary>
        ///     The expression tree on the left of side of the binary operation.
        /// </summary>
        public ILSLExprNode LeftExpression { get; private set; }

        /// <summary>
        ///     The expression tree on the right side of the binary operation.
        /// </summary>
        public ILSLExprNode RightExpression { get; private set; }

        /// <summary>
        ///     The source code range that encompasses the binary expression and its children.
        /// </summary>
        /// <remarks>
        ///     If <see cref="ILSLReadOnlySyntaxTreeNode.SourceRangesAvailable" /> is <c>false</c> this property will be
        ///     <c>null</c>.
        /// </remarks>
        public LSLSourceCodeRange SourceRangeOperation { get; private set; }

        ILSLReadOnlySyntaxTreeNode ILSLReadOnlySyntaxTreeNode.Parent
        {
            get { return Parent; }
        }

        /// <summary>
        ///     The binary operation type of this node.
        /// </summary>
        public LSLBinaryOperationType Operation { get; private set; }

        /// <summary>
        ///     The string representation of the binary operation this node preforms.
        /// </summary>
        public string OperationString { get; private set; }

        ILSLReadOnlyExprNode ILSLBinaryExpressionNode.LeftExpression
        {
            get { return LeftExpression; }
        }

        ILSLReadOnlyExprNode ILSLBinaryExpressionNode.RightExpression
        {
            get { return RightExpression; }
        }


        /// <summary>
        ///     Gets an instance of this node that represents a syntax error in the node.
        /// </summary>
        /// <param name="sourceRange">The source code range of the error.</param>
        /// <returns>An error form of the node.</returns>
        public static
            LSLBinaryExpressionNode GetError(LSLSourceCodeRange sourceRange)
        {
            return new LSLBinaryExpressionNode(sourceRange, Err.Err);
        }


        private void ParseAndSetOperation(string operationString)
        {
            OperationString = operationString;
            Operation = LSLBinaryOperationTypeTools.ParseFromOperator(OperationString);
        }

        #region Nested type: Err

        private enum Err
        {
            Err
        }

        #endregion

        #region ILSLExprNode Members

        /// <summary>
        ///     True if this syntax tree node contains syntax errors. <para/>
        ///     <see cref="SourceRange"/> should point to a more specific error location when this is <c>true</c>. <para/>
        ///     Other source ranges will not be available.
        /// </summary>
        public bool HasErrors { get; internal set; }


        /// <summary>
        ///     The source code range that this syntax tree node occupies.
        /// </summary>
        /// <remarks>
        ///     If <see cref="ILSLReadOnlySyntaxTreeNode.SourceRangesAvailable" /> is <c>false</c> this property will be
        ///     <c>null</c>.
        /// </remarks>
        public LSLSourceCodeRange SourceRange { get; private set; }


        /// <summary>
        ///     Should return true if source code ranges are available/set to meaningful values
        ///     for this node.
        /// </summary>
        public bool SourceRangesAvailable { get; private set; }


        /// <summary>
        ///     Accept a visit from an implementor of <see cref="ILSLValidatorNodeVisitor{T}" />
        /// </summary>
        /// <typeparam name="T">The visitors return type.</typeparam>
        /// <param name="visitor">The visitor instance.</param>
        /// <returns>The value returned from this method in the visitor used to visit this node.</returns>
        public T AcceptVisitor<T>(ILSLValidatorNodeVisitor<T> visitor)
        {
            return visitor.VisitBinaryExpression(this);
        }


        /// <summary>
        ///     True if the expression statement has some modifying effect on a local parameter or global/local variable;  or is a
        ///     function call.  False otherwise.
        /// </summary>
        public bool HasPossibleSideEffects
        {
            get
            {
                if (LeftExpression == null || RightExpression == null) return false;

                var eitherSideHaveSideEffects = (LeftExpression.HasPossibleSideEffects ||
                                                 RightExpression.HasPossibleSideEffects);

                var operatorModifiesLeftVariable = LeftExpression.IsModifiableLeftValue() &&
                                                   Operation.IsAssignOrModifyAssign();

                return (eitherSideHaveSideEffects || operatorModifiesLeftVariable);
            }
        }


        /// <summary>
        ///     Should produce a user friendly description of the expressions return type. <para/>
        ///     This is used in some syntax error messages, Ideally you should enclose your description in
        ///     parenthesis or something that will make it stand out in a string.
        /// </summary>
        /// <returns>A use friendly description of the node's <see cref="Type"/>.</returns>
        public string DescribeType()
        {
            return "(" + Type + (this.IsLiteral() ? " Literal)" : ")");
        }


        /// <summary>
        ///     The return type of the expression. see: <see cref="LSLType" />
        /// </summary>
        public LSLType Type { get; private set; }


        /// <summary>
        ///     The expression type/classification of the expression. see: <see cref="LSLExpressionType" />
        /// </summary>
        /// <value>
        ///     The type of the expression.
        /// </value>
        public LSLExpressionType ExpressionType
        {
            get { return LSLExpressionType.BinaryExpression; }
        }


        /// <summary>
        ///     True if the expression is constant and can be calculated at compile time.
        /// </summary>
        public bool IsConstant
        {
            get
            {
                return
                    (LeftExpression != null && RightExpression != null) &&
                    (LeftExpression.IsConstant && RightExpression.IsConstant);
            }
        }


        ILSLReadOnlyExprNode ILSLReadOnlyExprNode.Clone()
        {
            return Clone();
        }


        /// <summary>
        ///     The parent node of this syntax tree node.
        /// </summary>
        /// <exception cref="InvalidOperationException" accessor="set">If Parent has already been set.</exception>
        /// <exception cref="ArgumentNullException" accessor="set"><paramref name="value" /> is <c>null</c>.</exception>
        public ILSLSyntaxTreeNode Parent
        {
            get { return _parent; }
            set
            {
                if (_parent != null)
                {
                    throw new InvalidOperationException(GetType().Name +
                                                        ": Parent node already set, it can only be set once.");
                }
                if (value == null)
                {
                    throw new ArgumentNullException("value", GetType().Name + ": Parent cannot be set to null.");
                }

                _parent = value;
            }
        }


        /// <summary>
        ///     Deep clones the expression node.  It should clone the node and all of its children and cloneable properties, except
        ///     the parent.
        ///     When cloned, the parent node reference should be left <c>null</c>.
        /// </summary>
        /// <returns>A deep clone of this expression tree node.</returns>
        public LSLBinaryExpressionNode Clone()
        {
            return HasErrors ? GetError(SourceRange) : new LSLBinaryExpressionNode(this);
        }


        ILSLExprNode ILSLExprNode.Clone()
        {
            return Clone();
        }

        #endregion
    }
}