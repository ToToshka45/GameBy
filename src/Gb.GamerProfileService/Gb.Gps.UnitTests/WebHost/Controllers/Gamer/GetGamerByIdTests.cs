using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using GamerProfileService.Controllers;
using GamerProfileService.Models;
using Gb.Gps.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services.Abstractions;
using Services.Contracts.Gamer;

namespace Gb.Gps.UnitTests.WebHost.Controllers.Gamer;

public class GetGamerByIdTests
{
    private readonly GamerController _gamerController;
    private readonly Mock<ICacheService> _cacheServiceMock;
    private readonly Mock<IGamerService> _gamerServiceMock;

    public GetGamerByIdTests()
    {
        var fixture = new Fixture().Customize( new AutoMoqCustomization() );
        _gamerServiceMock = fixture.Freeze<Mock<IGamerService>>();
        _cacheServiceMock = fixture.Freeze<Mock<ICacheService>>();
        _gamerController = fixture.Build<GamerController>().OmitAutoProperties().Create();
    }

    [Fact]
    public async void GetAsync_GamerIsNotFound_ReturnsNotFound()
    {
        // Arrange
        var gamerId = 1;
        GamerModel nullGM = null;
        GamerDto nullGD = null;

        _cacheServiceMock
            .Setup( repo => repo.GetAsync<GamerModel>( "Gamer_" + gamerId, CancellationToken.None ) )
            .ReturnsAsync( nullGM );

        _gamerServiceMock
            .Setup( repo => repo.GetByIdAsync( gamerId, CancellationToken.None ) )
            .ReturnsAsync( nullGD );

        // Act
        var result = await _gamerController.GetAsync( gamerId, CancellationToken.None );

        // Assert
        result.Result.Should().BeAssignableTo<NotFoundObjectResult>();
    }

    [Fact]
    public async void GetAsync_GamerIsNotFoundInCacheFoundInDB_ReturnsOk()
    {
        // Arrange
        var gamerId = 1;
        GamerModel nullGM = null;
        GamerDto gd = new GamerDto { Id = gamerId, Name = "Anatoliy" };

        _cacheServiceMock
            .Setup( repo => repo.GetAsync<GamerModel>( "Gamer_" + gamerId, CancellationToken.None ) )
            .ReturnsAsync( nullGM );

        _gamerServiceMock
            .Setup( repo => repo.GetByIdAsync( gamerId, CancellationToken.None ) )
            .ReturnsAsync( gd );

        // Act
        var result = await _gamerController.GetAsync( gamerId, CancellationToken.None );

        // Assert
        result.Result.Should().BeAssignableTo<OkObjectResult>();
    }

    [Fact]
    public async void GetAsync_GamerIsFoundInCache_ReturnsOk()
    {
        // Arrange
        var gamerId = 1;
        GamerModel gm = new() { Id = gamerId, Name = "Anatoliy" };

        _cacheServiceMock
            .Setup( repo => repo.GetAsync<GamerModel>( "Gamer_" + gamerId, CancellationToken.None ) )
            .ReturnsAsync( gm );

        // Act
        var result = await _gamerController.GetAsync( gamerId, CancellationToken.None );

        // Assert
        result.Result.Should().BeAssignableTo<OkObjectResult>();
    }
}