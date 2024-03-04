using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class TblHistorySearch
{
    public int Id { get; set; }

    public int? IdUser { get; set; }

    public int? IdWord { get; set; }

    public DateTime? DDatetime { get; set; }

    public virtual TblUser? IdUserNavigation { get; set; }

    public virtual TblWord? IdWordNavigation { get; set; }
}
