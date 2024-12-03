using System;
using System.Collections.Generic;

namespace Farkas_Zoltán_bckend.Models;

public partial class Actor
{
    public int ActorId { get; set; }

    public string? ActorName { get; set; }

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
