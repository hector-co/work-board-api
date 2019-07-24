﻿using System;
using System.Collections.Generic;
using System.Linq;
using WorkBoard.Application.Dtos;
using WorkBoard.DataAccess.Ef.BoardDataAccess;

namespace WorkBoard.DataAccess.Ef
{
    public class DbInitializer
    {
        public static void AddSampleData(WorkBoardContext context)
        {
            if (!context.Set<UserDto>().Any())
            {
                context.Set<UserDto>().AddRange(new[]
                {
                    // 1
                    new UserDto
                    {
                        Name = "User1",
                        LastName = "LastName1",
                        Email = "user1@mail.com",
                        Username = "user1",
                        Password = "",
                        Veryfied = true,
                        Guid = Guid.NewGuid(),
                        Version = 1
                    },
                    // 2
                    new UserDto
                    {
                        Name = "User2",
                        LastName = "LastName2",
                        Email = "user2@mail.com",
                        Username = "user2",
                        Password = "",
                        Veryfied = true,
                        Guid = Guid.NewGuid(),
                        Version = 1
                    },
                    // 3
                    new UserDto
                    {
                        Name = "User3",
                        LastName = "LastName3",
                        Email = "user3@mail.com",
                        Username = "user3",
                        Password = "",
                        Veryfied = true,
                        Guid = Guid.NewGuid(),
                        Version = 1
                    }
                });
                context.SaveChanges();
            }

            if (!context.Set<BoardDtoDataAccess>().Any())
            {
                context.Set<BoardDtoDataAccess>().AddRange(new[]
                {
                    // 1
                    new BoardDtoDataAccess
                    {
                        Title = "Project1",
                        UsersDataAccess = new List<BoardDtoDataAccessUserDto>
                        {
                            new BoardDtoDataAccessUserDto
                            {
                                UserId = 1
                            }
                        },
                        Columns = new List<BoardColumnDto>
                        {
                            new BoardColumnDto
                            {
                                Title = "To do",
                                Description = "",
                                Active = true
                            },
                            new BoardColumnDto
                            {
                                Title = "In progress",
                                Description = "",
                                Active = true
                            },
                            new BoardColumnDto
                            {
                                Title = "Done",
                                Description = "",
                                Active = true
                            }
                        },
                        Description = "",
                        State = BoardState.Open,
                        Guid = Guid.NewGuid(),
                        Version = 1
                    },
                    // 2
                    new BoardDtoDataAccess
                    {
                        Title = "Project2",
                        UsersDataAccess = new List<BoardDtoDataAccessUserDto>
                        {
                            new BoardDtoDataAccessUserDto
                            {
                                UserId = 1
                            },
                            new BoardDtoDataAccessUserDto
                            {
                                UserId = 2
                            },
                            new BoardDtoDataAccessUserDto
                            {
                                UserId = 3
                            }
                        },
                        Columns =
                        {
                            new BoardColumnDto
                            {
                                Title = "To do",
                                Description = "",
                                Active = true
                            },
                            new BoardColumnDto
                            {
                                Title = "In progress",
                                Description = "",
                                Active = true
                            },
                            new BoardColumnDto
                            {
                                Title = "Done",
                                Description = "",
                                Active = true
                            }
                        },
                        Description = "",
                        State = BoardState.Open,
                        Guid = Guid.NewGuid(),
                        Version = 1
                    }
                });
                context.SaveChanges();
            }
        }
    }
}
