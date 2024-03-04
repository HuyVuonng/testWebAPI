using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class TblWordType
{
    public int Id { get; set; }

    public string SWordtype { get; set; } = null!;

    public virtual ICollection<TblWord> TblWords { get; set; } = new List<TblWord>();
}
