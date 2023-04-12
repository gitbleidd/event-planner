using AutoFixture;
using EventPlanner.App.Models;
using EventPlanner.App.Services;
using FluentAssertions;

namespace EventPlanner.UnitTests.ServicesTests;

public class ParticipantSelectionServiceTests
{
    [Fact]
    public void ParticipantSelectionService_WhenCantTakeAnyone_ShouldEmpty()
    {
        // Arrange
        int slots = 0;
        var registeredUsers = new Fixture()
            .CreateMany<ParticipantSelectionModel>(10)
            .ToList();

        registeredUsers.ForEach(p => p.TakenExtraUsersCount = 10);
        var participantSelectionService = new ParticipantSelectionService();

        // Act
        var participants = participantSelectionService.GetParticipants(registeredUsers, slots);
        
        // Assert
        participants.Should().BeEmpty();
    }
    
    [Fact]
    public void ParticipantSelectionService_WhenTakenExtraUsersCountIsZero_ShouldAllSeatsCanBeTaken()
    {
        // Arrange
        int slots = 15;
        var registeredUsers = new Fixture()
            .CreateMany<ParticipantSelectionModel>(20)
            .ToList();

        registeredUsers.ForEach(p =>
        {
            p.TakenExtraUsersCount = 0;
            p.Weight = 1;
        });
        
        var participantSelectionService = new ParticipantSelectionService();

        // Act
        var participants = participantSelectionService.GetParticipants(registeredUsers.ToList(), slots);
        
        // Assert
        var countSeatsTaken = registeredUsers
            .Where(x => participants.Contains(x.UserInfo))
            .Sum(x => x.TakenExtraUsersCount + 1);

        countSeatsTaken.Should().Be(slots);
    }
    
    
    [Fact]
    public void ParticipantSelectionService_WhenTakenExtraUsersCountIsOne_ShouldNotAllSeatsCanBeTaken()
    {
        // Arrange
        int slots = 15;
        var registeredUsers = new Fixture()
            .CreateMany<ParticipantSelectionModel>(20)
            .ToList();

        registeredUsers.ForEach(p =>
        {
            p.TakenExtraUsersCount = 1;
            p.Weight = 1;
        });
        
        var participantSelectionService = new ParticipantSelectionService();

        // Act
        var participants = participantSelectionService.GetParticipants(registeredUsers.ToList(), slots);
        
        // Assert
        var anotherUsers = registeredUsers
            .Where(x => !participants.Contains(x.UserInfo));
        
        anotherUsers.Should().HaveCount(13);
    }
}