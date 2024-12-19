using AutoFixture;
using AutoFixture.AutoMoq;
using GamerProfileService.Controllers;
using Moq;
using Gb.Gps.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using YamlDotNet.Core;
using Services.Repositories.Abstractions;
using GamerProfileService.Models;
using FluentAssertions;
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
            .Setup( repo => repo.GetAsync<GamerModel>( "Gamer_1", CancellationToken.None ) )
            .ReturnsAsync( nullGM );

        _gamerServiceMock
            .Setup( repo => repo.GetByIdAsync( 1, CancellationToken.None ) )
            .ReturnsAsync( nullGD );

        // Act
        var result = await _gamerController.GetAsync( gamerId, CancellationToken.None );

        // Assert
        result.Result.Should().BeAssignableTo<NotFoundObjectResult>();
    }
}