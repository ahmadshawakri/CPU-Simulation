using System;
using Xunit;
using System.IO;

namespace CPUManager;

public class HandlEFilesIOTest
{
    [Fact]
    public void ReadJsonFile_ValidJson_ReturnsDeserializedObject()
    {
        // Arrange
        string jsonContent =
            @"
            {
                ""NumOfProcessors"": 4,
                ""Tasks"": [
                    {
                    ""Id"": ""t1"",
                    ""CreationTime"": 4,
                    ""RequestedTime"": 3,
                    ""Priority"": ""High""
                    },
                    {
                    ""Id"": ""t2"",
                    ""CreationTime"": 3,
                    ""RequestedTime"": 4,
                    ""Priority"": ""Low""
                    },
                    {
                    ""Id"": ""t3"",
                    ""CreationTime"": 2,
                    ""RequestedTime"": 5,
                    ""Priority"": ""High""
                    }
                ]
            }";
        string fileName = "test.json";
        File.WriteAllText(fileName, jsonContent);

        // Act
        HandleFilesIO readFileJson = new HandleFilesIO();
        Simulator resultData = readFileJson.ReadFile<Simulator>(fileName);

        // Assert
        Assert.NotNull(resultData);
        Assert.Equal(4, resultData.NumOfProcessors);
        Assert.Equal(3, resultData?.Tasks?.Count);

        //Clean up
        File.Delete(fileName);
    }

    [Fact]
    public void ReadXmlFile_ValidXml_ReturnsDeserializedObject()
    {
        // Arrange
        string xmlContent =
            @"<SimulationConfig>
                <NumOfProcessors>4</NumOfProcessors>
                <Tasks>
                    <Task>
                        <Id>T1</Id>
                        <CreationTime>2</CreationTime>
                        <RequestedTime>2</RequestedTime>
                        <Priority>High</Priority>
                    </Task>
                    <Task>
                        <Id>T2</Id>
                        <CreationTime>5</CreationTime>
                        <RequestedTime>3</RequestedTime>
                        <Priority>Low</Priority>
                    </Task>
                    <Task>
                        <Id>T3</Id>
                        <CreationTime>4</CreationTime>
                        <RequestedTime>7</RequestedTime>
                        <Priority>Low</Priority>
                    </Task>
                </Tasks>
            </SimulationConfig>";
        string filename = "test.xml";
        File.WriteAllText(filename, xmlContent);

        // Act
        HandleFilesIO readFileXML = new HandleFilesIO();
        Simulator resultData = readFileXML.ReadFile<Simulator>(filename);

        // Assert
        Assert.NotNull(resultData);
        Assert.Equal(4, resultData.NumOfProcessors);
        Assert.Equal(3, resultData.Tasks?.Count);

        // Cleanup
        File.Delete(filename);
    }

    [Fact]
    public void ReadFile_UnsupportedExtension_ThrowsArgumentException()
    {
        // Arrange
        string filename = "test.txt";

        // Act
        HandleFilesIO fileReader = new HandleFilesIO();
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () => fileReader.ReadFile<Simulator>(filename)
        );

        // Assert
        Assert.Matches($"Unsupported file extension: .txt", exception.Message);
    }
}
