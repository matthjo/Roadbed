namespace Roadbed.Test.Unit.Common;

/// <summary>
/// Contains unit tests for verifying the behavior of the CommonStringExtensions class.
/// </summary>
[TestClass]
public class CommonStringExtensionTests
{
    #region Public Methods

    /// <summary>
    /// Unit test to verify that the CommonEnvironment can be determined by "LOCAL".
    /// </summary>
    [TestMethod]
    public void CommonStringExtension_CommonEnvironment_VerifyLocalResult()
    {
        // Arrange (Given)
        CommonEnvironmentType expectedEnum = CommonEnvironmentType.Local;
        string actualString = "LOCAL";

        // Act (When)
        CommonEnvironmentType actualEnum = actualString.GetCommonEnvironment();

        // Assert (Then)
        Assert.AreEqual(
            expectedEnum,
            actualEnum,
            "CommonEnvironment could be determined by \"LOCAL\".");
    }

    #endregion Public Methods
}