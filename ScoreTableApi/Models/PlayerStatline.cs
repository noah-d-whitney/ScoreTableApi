﻿using System;
using System.Collections.Generic;

namespace ScoreTableApi.Models;

public class PlayerStatline
{
    public int Id { get; set; }

    public bool IsStarter { get; set; }

    public int GameId { get; set; }

    public int PlayerId { get; set; }
    public int TeamId { get; set; }
    public Team Team { get; set; }

    public int Points { get; set; }

    public int Rebounds { get; set; }

    public int Assists { get; set; }

    public int Steals { get; set; }

    public int Blocks { get; set; }

    public int Fouls { get; set; }

    public int Turnovers { get; set; }

    public int Fga { get; set; }

    public int Fgm { get; set; }

    public int Fta { get; set; }

    public int Ftm { get; set; }

    public int Tpa { get; set; }

    public int Tpm { get; set; }

    public virtual Game Game { get; set; } = null!;

    public virtual Player Player { get; set; } = null!;

    public string UserId { get; set; }
    public User User { get; set; }
}
