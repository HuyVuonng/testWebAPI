using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class TblWord
{
    public int Id { get; set; }

    public int? IdLanguage { get; set; }

    public int? IdLanguageTrans { get; set; }

    public int? IdWordtype { get; set; }

    public int? IdUser { get; set; }

    public string SWord { get; set; } = null!;

    public string? SExample { get; set; }

    public string? SDefinition { get; set; }
    public string? sWordTrans { get; set; }

    public virtual TblLanguage? IdLanguageNavigation { get; set; }

    public virtual TblLanguage? IdLanguageTransNavigation { get; set; }

    public virtual TblUser? IdUserNavigation { get; set; }

    public virtual TblWordType? IdWordtypeNavigation { get; set; }

    public virtual ICollection<TblHistorySearch> TblHistorySearches { get; set; } = new List<TblHistorySearch>();
}
