using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class TblUser
{
    public int Id { get; set; }

    public string SEmail { get; set; } = null!;

    public string SPassword { get; set; } = null!;

    public string? SRole { get; set; }

    public virtual ICollection<TblHistorySearch> TblHistorySearches { get; set; } = new List<TblHistorySearch>();

    public virtual ICollection<TblWord> TblWords { get; set; } = new List<TblWord>();
}
