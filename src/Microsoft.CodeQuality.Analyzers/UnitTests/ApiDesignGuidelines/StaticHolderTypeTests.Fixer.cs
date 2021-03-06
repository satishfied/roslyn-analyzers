﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using Microsoft.CodeQuality.CSharp.Analyzers.ApiDesignGuidelines;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Test.Utilities;
using Xunit;

namespace Microsoft.CodeQuality.Analyzers.ApiDesignGuidelines.UnitTests
{
    public class StaticHolderTypeFixerTests : CodeFixTestBase
    {
        protected override CodeFixProvider GetBasicCodeFixProvider()
        {
            throw new NotImplementedException();
        }

        protected override DiagnosticAnalyzer GetBasicDiagnosticAnalyzer()
        {
            return new StaticHolderTypesAnalyzer();
        }

        protected override CodeFixProvider GetCSharpCodeFixProvider()
        {
            return new CSharpStaticHolderTypesFixer();
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new StaticHolderTypesAnalyzer();
        }

        [Fact]
        public void CA1052FixesNonStaticClassWithOnlyStaticDeclaredMembersCSharp()
        {
            const string Code = @"
public class C
{
    public static void Foo() { }
}
";

            const string FixedCode = @"
public static class C
{
    public static void Foo() { }
}
";

            VerifyCSharpFix(Code, FixedCode);
        }

        [Fact]
        public void CA1052FixesNonStaticClassWithPublicDefaultConstructorAndStaticMethodCSharp()
        {
            const string Code = @"
public class C
{
    public C() { }
    public static void Foo() { }
}
";

            const string FixedCode = @"
public static class C
{
    public static void Foo() { }
}
";

            VerifyCSharpFix(Code, FixedCode);
        }

        [Fact]
        public void CA1052FixesNonStaticClassWithProtectedDefaultConstructorAndStaticMethodCSharp()
        {
            const string Code = @"
public class C
{
    protected C() { }
    public static void Foo() { }
}
";

            const string FixedCode = @"
public static class C
{
    public static void Foo() { }
}
";

            VerifyCSharpFix(Code, FixedCode);
        }

        [Fact]
        public void CA1052FixesNonStaticClassWithPrivateDefaultConstructorAndStaticMethodCSharp()
        {
            const string Code = @"
public class C
{
    private C() { }
    public static void Foo() { }
}
";

            const string FixedCode = @"
public static class C
{
    public static void Foo() { }
}
";

            VerifyCSharpFix(Code, FixedCode);
        }

        [Fact]
        public void CA1052FixesNestedPublicNonStaticClassWithPublicDefaultConstructorAndStaticMethodCSharp()
        {
            const string Code = @"
public class C
{
    public void Moo() { }

    public class CInner
    {
        public CInner() { }
        public static void Foo() { }
    }
}
";

            const string FixedCode = @"
public class C
{
    public void Moo() { }

    public static class CInner
    {
        public static void Foo() { }
    }
}
";

            VerifyCSharpFix(Code, FixedCode);
        }

        [Fact]
        public void CA1052FixesNestedPublicClassInOtherwiseEmptyNonStaticClassCSharp()
        {
            const string Code = @"
public class C
{
    public class CInner
    {
    }
}
";

            const string FixedCode = @"
public static class C
{
    public class CInner
    {
    }
}
";

            VerifyCSharpFix(Code, FixedCode);
        }
    }
}
