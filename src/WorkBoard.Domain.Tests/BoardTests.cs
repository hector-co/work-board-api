using FluentAssertions;
using Hco.Base.Domain.Exceptions;
using System;
using WorkBoard.Domain.Model;
using Xunit;

namespace WorkBoard.Domain.Tests
{
    public class BoardTests
    {
        [Fact]
        public void BoardIsCreatedOpenByDefault()
        {
            var board = new Board("test board", "");
            board.IsOpen().Should().BeTrue();
        }

        [Fact]
        public void ClosingBoardShouldUpdateState()
        {
            var board = new Board("test board", "");
            board.Close();
            board.IsOpen().Should().BeFalse();
        }

        [Fact]
        public void ReOpenBoardWhenStateIsOpenShouldThrowDomainException()
        {
            var board = new Board("test board", "");
            Action reOpen = () => board.ReOpen();
            reOpen.Should().Throw<DomainException>();
        }

        [Fact]
        public void ClosingBoardWhenStateIsClosedShouldThrowDomainException()
        {
            var board = new Board("test board", "");
            board.Close();
            Action close = () => board.Close();
            close.Should().Throw<DomainException>();
        }
    }
}
