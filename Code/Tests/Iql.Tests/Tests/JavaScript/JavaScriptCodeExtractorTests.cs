using System;
using Iql.JavaScript;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests.JavaScript
{
    [TestClass]
    public class JavaScriptCodeExtractorTests
    {
        [TestMethod]
        public void TestParameterless()
        {
            var expression = "something.Owner.Name == somethingElse.Description";
            var fn = $"function () {{ return {expression}; }}";
            var body = JavaScriptCodeExtractor.ExtractBody(fn);
            Assert.AreEqual(0, body.ParameterNames.Length);
        }

        [TestMethod]
        public void TestParameterlessEs6()
        {
            var expression = "something.Owner.Name == somethingElse.Description";
            var fn = $"() => {expression};";
            var body = JavaScriptCodeExtractor.ExtractBody(fn);
            Assert.AreEqual(0, body.ParameterNames.Length);
        }

        [TestMethod]
        public void TestEs2015()
        {
            var body = @"var yourNameIs = ""Marta"";
var myNameIs = ""Paulina""";
            var code = $@"function(a, b, c) {{
    {body};
}}";
            var expectedResult = new JavaScriptFunctionBody(
                body, "a, b, c", code, $@"function (a, b, c) {{ {body}; }}");
            var actualResult = JavaScriptCodeExtractor.ExtractBody(code, false);
            AssertResult(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestEs6()
        {
            var body = "p.FullName";
            var code = $"p => {body}";
            var expectedResult = new JavaScriptFunctionBody(
                body, "p", code, "function (p) { return p.FullName; }");
            var actualResult = JavaScriptCodeExtractor.ExtractBody(code);
            AssertResult(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestEs6Speed()
        {
            var start = DateTime.Now;
            var count = 500000;
#if TypeScript
            count = 50000;
#endif
            for (var i = 0; i < count; i++)
            {
//                var actualResult = JavaScriptCodeExtractor.ExtractBody(@"function(p) { 
//    // Some comment
//    var quote = 'Some quote with a /* comment */';
//    return /* some other comment */ p.FullName; 
//}");
                var actualResult = JavaScriptCodeExtractor.ExtractBody(@"p => p.FullName");
            }
            var time = DateTime.Now - start;
#if TypeScript
            Assert.IsTrue(time.TotalMilliseconds < 1600);
#else
            Assert.IsTrue(time.TotalMilliseconds < 4000);
#endif
        }

        [TestMethod]
        public void TestRemoveComments()
        {
            var code = @"/**
 * @license
 * Copyright Google Inc. All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.io/license
 */
import { __core_private__ as r } from '@angular/core';
export var /** @type {?} */ RenderDebugInfo = r.RenderDebugInfo;
export var /** @type {?} */ ReflectionCapabilities = r.ReflectionCapabilities;
export var /** @type {?} */ DebugDomRootRenderer = r.DebugDomRootRenderer;
export var /** @type {?} */ reflector = r.reflector;
export var /** @type {?} */ NoOpAnimationPlayer = r.NoOpAnimationPlayer;
export var /** @type {?} */ AnimationPlayer = r.AnimationPlayer;
export var /** @type {?} */ AnimationSequencePlayer = r.AnimationSequencePlayer;
export var /** @type {?} */ AnimationGroupPlayer = r.AnimationGroupPlayer;
export var /** @type {?} */ AnimationKeyframe = r.AnimationKeyframe;
export var /** @type {?} */ AnimationStyles = r.AnimationStyles;
export var /** @type {?} */ prepareFinalAnimationStyles = r.prepareFinalAnimationStyles;
export var /** @type {?} */ balanceAnimationKeyframes = r.balanceAnimationKeyframes;
export var /** @type {?} */ clearStyles = r.clearStyles;
export var /** @type {?} */ collectAndResolveStyles = r.collectAndResolveStyles;
//# sourceMappingURL=private_import_core.js.map";
            var cleaned = JavaScriptCodeExtractor.RemoveComments(code);
            var expected = @"
import { __core_private__ as r } from '@angular/core';
export var  RenderDebugInfo = r.RenderDebugInfo;
export var  ReflectionCapabilities = r.ReflectionCapabilities;
export var  DebugDomRootRenderer = r.DebugDomRootRenderer;
export var  reflector = r.reflector;
export var  NoOpAnimationPlayer = r.NoOpAnimationPlayer;
export var  AnimationPlayer = r.AnimationPlayer;
export var  AnimationSequencePlayer = r.AnimationSequencePlayer;
export var  AnimationGroupPlayer = r.AnimationGroupPlayer;
export var  AnimationKeyframe = r.AnimationKeyframe;
export var  AnimationStyles = r.AnimationStyles;
export var  prepareFinalAnimationStyles = r.prepareFinalAnimationStyles;
export var  balanceAnimationKeyframes = r.balanceAnimationKeyframes;
export var  clearStyles = r.clearStyles;
export var  collectAndResolveStyles = r.collectAndResolveStyles;
";
            Assert.AreEqual(expected.Replace("\r", ""), cleaned.Replace("\r", ""));
        }

        [TestMethod]
        public void TestRemoveComments2()
        {
            var code = @"function (p, x) { /*some comment*/ return p.FullName; }";
            var cleaned = JavaScriptCodeExtractor.RemoveComments(code);
            var expected = @"function (p, x) {  return p.FullName; }";
            Assert.AreEqual(expected.Replace("\r", ""), cleaned);
        }

        private void AssertResult(JavaScriptFunctionBody expectedResult, JavaScriptFunctionBody actualResult)
        {
            Assert.AreEqual(expectedResult.Body, actualResult.Body);
            Assert.AreEqual(expectedResult.ParameterNames.Length, actualResult.ParameterNames.Length);
            for (var i = 0; i < expectedResult.ParameterNames.Length; i++)
            {
                Assert.AreEqual(expectedResult.ParameterNames[i], actualResult.ParameterNames[i]);
            }
            Assert.AreEqual(expectedResult.OriginalCode, actualResult.OriginalCode);
            Assert.AreEqual(expectedResult.CleanedCode, actualResult.CleanedCode);
            Assert.AreEqual(expectedResult.Signature, actualResult.Signature);
        }
    }
}