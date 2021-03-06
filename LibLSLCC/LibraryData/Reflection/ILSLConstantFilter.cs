#region FileInfo

// 
// File: ILSLConstantFilter.cs
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

using System.Reflection;

#endregion

namespace LibLSLCC.LibraryData.Reflection
{
    /// <summary>
    ///     Allows modification of a constant signature after its basic information has been serialized, before its returned.
    /// </summary>
    public interface ILSLConstantFilter
    {
        /// <summary>
        ///     Allows <see cref="PropertyInfo" /> objects to be prematurely filtered from de-serialization output.  Returns
        ///     <c>true</c> if the <see cref="PropertyInfo" /> should be filtered.
        /// </summary>
        /// <param name="serializer">The <see cref="LSLLibraryDataReflectionSerializer" /> this add-on belongs to.</param>
        /// <param name="info">The <see cref="PropertyInfo" /> object we may want to filter from the output.</param>
        /// <returns><c>true</c> if the property needs to be filtered from the results.</returns>
        bool PreFilter(LSLLibraryDataReflectionSerializer serializer, PropertyInfo info);


        /// <summary>
        ///     Allows <see cref="FieldInfo" /> objects to be prematurely filtered from de-serialization output.  Returns
        ///     <c>true</c> if the <see cref="MethodInfo" /> should be filtered.
        /// </summary>
        /// <param name="serializer">The <see cref="LSLLibraryDataReflectionSerializer" /> this add-on belongs to.</param>
        /// <param name="info">The <see cref="PropertyInfo" /> object we may want to filter from the output.</param>
        /// <returns><c>true</c> if the field needs to be filtered from the results.</returns>
        bool PreFilter(LSLLibraryDataReflectionSerializer serializer, FieldInfo info);


        /// <summary>
        ///     Allows modification of a constant signature after its basic information has been serialized from an objects
        ///     Property, before its returned.
        /// </summary>
        /// <param name="serializer">The <see cref="LSLLibraryDataReflectionSerializer" /> this add-on belongs to.</param>
        /// <param name="info">The <see cref="PropertyInfo" /> object the library constant signature was serialized from.</param>
        /// <param name="signature">The signature.</param>
        /// <returns><c>true</c> if the constant needs to be filtered from the results.</returns>
        bool MutateSignature(LSLLibraryDataReflectionSerializer serializer, PropertyInfo info,
            LSLLibraryConstantSignature signature);


        /// <summary>
        ///     Allows modification of a constant signature after its basic information has been serialized from an objects
        ///     Property, before its returned.
        /// </summary>
        /// <param name="serializer">The <see cref="LSLLibraryDataReflectionSerializer" /> this add-on belongs to.</param>
        /// <param name="info">The <see cref="FieldInfo" /> object the library constant signature was serialized from.</param>
        /// <param name="signature">The signature.</param>
        /// <returns><c>true</c> if the constant needs to be filtered from the results.</returns>
        bool MutateSignature(LSLLibraryDataReflectionSerializer serializer, FieldInfo info,
            LSLLibraryConstantSignature signature);
    }
}