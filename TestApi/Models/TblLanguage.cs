using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class TblLanguage
{
    public int Id { get; set; }

    public string SLanguage { get; set; } = null!;

    public virtual ICollection<TblWord> TblWordIdLanguageNavigations { get; set; } = new List<TblWord>();

    public virtual ICollection<TblWord> TblWordIdLanguageTransNavigations { get; set; } = new List<TblWord>();
}
