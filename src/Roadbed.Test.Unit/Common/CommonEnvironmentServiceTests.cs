namespace Roadbed.Test.Unit;

using Roadbed.Common.Services;

/// <summary>
/// Contains unit tests for verifying the behavior of the CommonEnvironmentService class.
/// </summary>
[TestClass]
public class CommonEnvironmentServiceTests
{
    #region Public Methods

    /// <summary>
    /// Unit test to verify that CommonEnvironmentService interprets "Development" correctly.
    /// </summary>
    [TestMethod]
    public void CommonEnvironmentService_DevEnvironment_PascalCaseValue()
    {
        // Arrange (Given)
        CommonEnvironmentType expectedEnumValue = CommonEnvironmentType.Development;
        string actualStringValue = "Development";

        // Act (When)
        CommonEnvironmentType actualEnumValue = CommonEnvironment.GetCommonEnvironment(actualStringValue);

        // Assert (Then)
        Assert.AreEqual(
            expectedEnumValue,
            actualEnumValue,
            "CommonEnvironment should be Development.");
    }

    /// <summary>
    /// Unit test to verify that CommonEnvironmentService interprets "DEV" correctly.
    /// </summary>
    [TestMethod]
    public void CommonEnvironmentService_DevEnvironment_UpperCaseValue()
    {
        // Arrange (Given)
        CommonEnvironmentType expectedEnumValue = CommonEnvironmentType.Development;
        string actualStringValue = "DEV";

        // Act (When)
        CommonEnvironmentType actualEnumValue = CommonEnvironment.GetCommonEnvironment(actualStringValue);

        // Assert (Then)
        Assert.AreEqual(
            expectedEnumValue,
            actualEnumValue,
            "CommonEnvironment should be Development.");
    }

    /// <summary>
    /// Unit test to verify that CommonEnvironmentService interprets "Local" correctly.
    /// </summary>
    [TestMethod]
    public void CommonEnvironmentService_LocalEnvironment_PascalCaseValue()
    {
        // Arrange (Given)
        CommonEnvironmentType expectedEnumValue = CommonEnvironmentType.Local;
        string actualStringValue = "Local";

        // Act (When)
        CommonEnvironmentType actualEnumValue = CommonEnvironment.GetCommonEnvironment(actualStringValue);

        // Assert (Then)
        Assert.AreEqual(
            expectedEnumValue,
            actualEnumValue,
            "CommonEnvironment should be Local.");
    }

    /// <summary>
    /// Unit test to verify that CommonEnvironmentService interprets "LOCAL" correctly.
    /// </summary>
    [TestMethod]
    public void CommonEnvironmentService_LocalEnvironment_UpperCaseValue()
    {
        // Arrange (Given)
        CommonEnvironmentType expectedEnumValue = CommonEnvironmentType.Local;
        string actualStringValue = "LOCAL";

        // Act (When)
        CommonEnvironmentType actualEnumValue = CommonEnvironment.GetCommonEnvironment(actualStringValue);

        // Assert (Then)
        Assert.AreEqual(
            expectedEnumValue,
            actualEnumValue,
            "CommonEnvironment should be Local.");
    }

    /// <summary>
    /// Unit test to verify that CommonEnvironmentService interprets "Production" correctly.
    /// </summary>
    [TestMethod]
    public void CommonEnvironmentService_ProductionEnvironment_PascalCaseValue()
    {
        // Arrange (Given)
        CommonEnvironmentType expectedEnumValue = CommonEnvironmentType.Production;
        string actualStringValue = "Production";

        // Act (When)
        CommonEnvironmentType actualEnumValue = CommonEnvironment.GetCommonEnvironment(actualStringValue);

        // Assert (Then)
        Assert.AreEqual(
            expectedEnumValue,
            actualEnumValue,
            "CommonEnvironment should be Production.");
    }

    /// <summary>
    /// Unit test to verify that CommonEnvironmentService interprets "PRO" correctly.
    /// </summary>
    [TestMethod]
    public void CommonEnvironmentService_ProductionEnvironment_UpperCaseValue()
    {
        // Arrange (Given)
        CommonEnvironmentType expectedEnumValue = CommonEnvironmentType.Production;
        string actualStringValue = "PRO";

        // Act (When)
        CommonEnvironmentType actualEnumValue = CommonEnvironment.GetCommonEnvironment(actualStringValue);

        // Assert (Then)
        Assert.AreEqual(
            expectedEnumValue,
            actualEnumValue,
            "CommonEnvironment should be Production.");
    }

    /// <summary>
    /// Unit test to verify that CommonEnvironmentService interprets "Test" correctly.
    /// </summary>
    [TestMethod]
    public void CommonEnvironmentService_QaEnvironment_PascalCaseValue()
    {
        // Arrange (Given)
        CommonEnvironmentType expectedEnumValue = CommonEnvironmentType.Qa;
        string actualStringValue = "Test";

        // Act (When)
        CommonEnvironmentType actualEnumValue = CommonEnvironment.GetCommonEnvironment(actualStringValue);

        // Assert (Then)
        Assert.AreEqual(
            expectedEnumValue,
            actualEnumValue,
            "CommonEnvironment should be Qa.");
    }

    /// <summary>
    /// Unit test to verify that CommonEnvironmentService interprets "QA" correctly.
    /// </summary>
    [TestMethod]
    public void CommonEnvironmentService_QaEnvironment_UpperCaseValue()
    {
        // Arrange (Given)
        CommonEnvironmentType expectedEnumValue = CommonEnvironmentType.Qa;
        string actualStringValue = "QA";

        // Act (When)
        CommonEnvironmentType actualEnumValue = CommonEnvironment.GetCommonEnvironment(actualStringValue);

        // Assert (Then)
        Assert.AreEqual(
            expectedEnumValue,
            actualEnumValue,
            "CommonEnvironment should be Qa.");
    }

    /// <summary>
    /// Unit test to verify that CommonEnvironmentService interprets "Staging" correctly.
    /// </summary>
    [TestMethod]
    public void CommonEnvironmentService_StagingEnvironment_PascalCaseValue()
    {
        // Arrange (Given)
        CommonEnvironmentType expectedEnumValue = CommonEnvironmentType.Staging;
        string actualStringValue = "Staging";

        // Act (When)
        CommonEnvironmentType actualEnumValue = CommonEnvironment.GetCommonEnvironment(actualStringValue);

        // Assert (Then)
        Assert.AreEqual(
            expectedEnumValue,
            actualEnumValue,
            "CommonEnvironment should be Staging.");
    }

    /// <summary>
    /// Unit test to verify that CommonEnvironmentService interprets "" correctly.
    /// </summary>
    [TestMethod]
    public void CommonEnvironmentService_UnknownEnvironment_BlankValue()
    {
        // Arrange (Given)
        CommonEnvironmentType expectedEnumValue = CommonEnvironmentType.Unknown;
        string actualStringValue = string.Empty;

        // Act (When)
        CommonEnvironmentType actualEnumValue = CommonEnvironment.GetCommonEnvironment(actualStringValue);

        // Assert (Then)
        Assert.AreEqual(
            expectedEnumValue,
            actualEnumValue,
            "CommonEnvironment should be Unknown.");
    }

    /// <summary>
    /// Unit test to verify that CommonEnvironmentService interprets "Super" correctly.
    /// </summary>
    [TestMethod]
    public void CommonEnvironmentService_UnknownEnvironment_SuperValue()
    {
        // Arrange (Given)
        CommonEnvironmentType expectedEnumValue = CommonEnvironmentType.Unknown;
        string actualStringValue = "Super";

        // Act (When)
        CommonEnvironmentType actualEnumValue = CommonEnvironment.GetCommonEnvironment(actualStringValue);

        // Assert (Then)
        Assert.AreEqual(
            expectedEnumValue,
            actualEnumValue,
            "CommonEnvironment should be Unknown.");
    }

    #endregion Public Methods
}