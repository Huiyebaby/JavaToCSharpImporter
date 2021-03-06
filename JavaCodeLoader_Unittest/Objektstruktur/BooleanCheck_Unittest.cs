﻿using CodeConverterCore.Enum;
using CodeConverterCore.Helper;
using CodeConverterJava.Model;
using CodeConverterCore.Model;
using NUnit.Framework;
using System.Collections.Generic;

namespace CodeConverterJava_Unittest.Objektstruktur
{
    public class BooleanCheck_Unittest
    {
        [Test]
        public void IfWIthEquals()
        {
            var tmpClass = @"
package org;
public class Class1 {
public void Run(){
if(true==true){}
}}";
            var tmpIniData = DataHelper.LoadIni("");
            var tmpObjectInformation = new JavaLoader().CreateObjectInformation(new List<string> { tmpClass }, tmpIniData);

            var tmpMethodeContent = tmpObjectInformation.ClassList[0].MethodeList[0].Code.CodeEntries[0] as StatementCode;
            var tmpIfCodeEntries = (tmpMethodeContent.StatementCodeBlocks[0].CodeEntries[0] as CodeExpression);
            Assert.AreEqual(2, tmpIfCodeEntries.SubClauseEntries.Count);
            Assert.AreEqual("true", tmpIfCodeEntries.SubClauseEntries[0].ToString());
            Assert.AreEqual(VariableOperatorType.Equals, tmpIfCodeEntries.Manipulator);
            Assert.AreEqual(StatementTypeEnum.If, tmpMethodeContent.StatementType);
        }

        [Test]
        public void IfWithOROfBooleanOperators()
        {
            var tmpClass = @"
package org;
public class Class1 {
public void Run(){
if(true||true){}
}}";
            var tmpIniData = DataHelper.LoadIni("");
            var tmpObjectInformation = new JavaLoader().CreateObjectInformation(new List<string> { tmpClass }, tmpIniData);

            var tmpMethodeContent = tmpObjectInformation.ClassList[0].MethodeList[0].Code.CodeEntries[0] as StatementCode;
            var tmpIfCodeEntries = (tmpMethodeContent.StatementCodeBlocks[0].CodeEntries[0] as CodeExpression);
            Assert.AreEqual(2, tmpIfCodeEntries.SubClauseEntries.Count);
            Assert.AreEqual(VariableOperatorType.Or, tmpIfCodeEntries.Manipulator);
            Assert.AreEqual("(true Or true)", tmpIfCodeEntries.ToString());
        }

        [Test]
        public void IfWithMultipleBooleanOperators1()
        {
            var tmpClass = @"
package org;
public class Class1 {
public void Run(){
if(true||true&&false){}
}}";
            var tmpIniData = DataHelper.LoadIni("");
            var tmpObjectInformation = new JavaLoader().CreateObjectInformation(new List<string> { tmpClass }, tmpIniData);

            var tmpMethodeContent = tmpObjectInformation.ClassList[0].MethodeList[0].Code.CodeEntries[0] as StatementCode;
            var tmpIfCodeEntries = (tmpMethodeContent.StatementCodeBlocks[0].CodeEntries[0] as CodeExpression);
            Assert.AreEqual(2, tmpIfCodeEntries.SubClauseEntries.Count);
            Assert.AreEqual(VariableOperatorType.Or, tmpIfCodeEntries.Manipulator);
            Assert.AreEqual("(true Or (true And false))", tmpIfCodeEntries.ToString());
        }

        [Test]
        public void IfWithMultipleBooleanOperators2()
        {
            var tmpClass = @"
package org;
public class Class1 {
public void Run(){
if(true&&true||false){}
}}";
            var tmpIniData = DataHelper.LoadIni("");
            var tmpObjectInformation = new JavaLoader().CreateObjectInformation(new List<string> { tmpClass }, tmpIniData);

            var tmpMethodeContent = tmpObjectInformation.ClassList[0].MethodeList[0].Code.CodeEntries[0] as StatementCode;
            var tmpIfCodeEntries = (tmpMethodeContent.StatementCodeBlocks[0].CodeEntries[0] as CodeExpression);
            Assert.AreEqual(2, tmpIfCodeEntries.SubClauseEntries.Count);
            Assert.AreEqual(VariableOperatorType.Or, tmpIfCodeEntries.Manipulator);
            Assert.AreEqual("((true And true) Or false)", tmpIfCodeEntries.ToString());
        }
    }
}
