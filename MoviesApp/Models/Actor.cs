﻿using System;

namespace MoviesApp.Models;

public class Actor
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime Birthday { get; set; }
}