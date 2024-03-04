using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BTL.Models;

public partial class DictionaryContext : DbContext
{
    public DictionaryContext()
    {
    }

    public DictionaryContext(DbContextOptions<DictionaryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblHistorySearch> TblHistorySearches { get; set; }
	public virtual DbSet<WordSearch> WordSearches { get; set; }


	public virtual DbSet<TblLanguage> TblLanguages { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    public virtual DbSet<TblWord> TblWords { get; set; }

    public virtual DbSet<TblWordType> TblWordTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("workstation id=DB_Back_End.mssql.somee.com;packet size=4096;user id=Huyvuong_SQLLogin_1;pwd=x76x6zwr4y;data source=DB_Back_End.mssql.somee.com;persist security info=False;initial catalog=DB_Back_End;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblHistorySearch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblHisto__3214EC07565BB64A");

            entity.ToTable("tblHistory_search");

            entity.Property(e => e.DDatetime)
                .HasColumnType("datetime")
                .HasColumnName("dDatetime");
            entity.Property(e => e.IdUser).HasColumnName("Id_user");
            entity.Property(e => e.IdWord).HasColumnName("Id_word");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.TblHistorySearches)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__tblHistor__Id_us__4222D4EF");

            entity.HasOne(d => d.IdWordNavigation).WithMany(p => p.TblHistorySearches)
                .HasForeignKey(d => d.IdWord)
                .HasConstraintName("FK__tblHistor__Id_wo__4316F928");
        });

        modelBuilder.Entity<TblLanguage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblLangu__3214EC078C201808");

            entity.ToTable("tblLanguage");

            entity.Property(e => e.SLanguage)
                .HasMaxLength(50)
                .HasColumnName("sLanguage");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblUser__3214EC071871F3C2");

            entity.ToTable("tblUser");

            entity.Property(e => e.SEmail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("sEmail");
            entity.Property(e => e.SPassword)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("sPassword");
            entity.Property(e => e.SRole)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sRole");
        });

        modelBuilder.Entity<TblWord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblWord__3214EC07203FC78A");

            entity.ToTable("tblWord");

            entity.Property(e => e.IdLanguage).HasColumnName("Id_Language");
            entity.Property(e => e.IdLanguageTrans).HasColumnName("Id_Language_trans");
            entity.Property(e => e.IdUser).HasColumnName("Id_user");
            entity.Property(e => e.IdWordtype).HasColumnName("Id_wordtype");
            entity.Property(e => e.SDefinition)
                .HasMaxLength(255)
                .HasColumnName("sDefinition");
            entity.Property(e => e.SExample)
                .HasMaxLength(255)
                .HasColumnName("sExample");
            entity.Property(e => e.SWord)
                .HasMaxLength(255)
                .HasColumnName("sWord");

            entity.HasOne(d => d.IdLanguageNavigation).WithMany(p => p.TblWordIdLanguageNavigations)
                .HasForeignKey(d => d.IdLanguage)
                .HasConstraintName("FK__tblWord__Id_Lang__3C69FB99");

            entity.HasOne(d => d.IdLanguageTransNavigation).WithMany(p => p.TblWordIdLanguageTransNavigations)
                .HasForeignKey(d => d.IdLanguageTrans)
                .HasConstraintName("FK__tblWord__Id_Lang__3D5E1FD2");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.TblWords)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__tblWord__Id_user__3F466844");

            entity.HasOne(d => d.IdWordtypeNavigation).WithMany(p => p.TblWords)
                .HasForeignKey(d => d.IdWordtype)
                .HasConstraintName("FK__tblWord__Id_word__3E52440B");
        });

        modelBuilder.Entity<TblWordType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblWord___3214EC0783B7530C");

            entity.ToTable("tblWord_type");

            entity.Property(e => e.SWordtype)
                .HasMaxLength(50)
                .HasColumnName("sWordtype");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public void addNewWord(int Id_Language, int Id_Language_trans, int Id_wordtype,int Id_user, string sWord, string sExample, string sDefinition,string sWordTrans)
    {

        string sql = "EXECUTE addNewWord @Id_Language,@Id_Language_trans,@Id_wordtype,@Id_user,@sWord,@sExample,@sDefinition, @sWordTrans";
        List<SqlParameter> parameters = new List<SqlParameter>
            {
                 new SqlParameter { ParameterName = "@Id_Language", Value = Id_Language },
                 new SqlParameter { ParameterName = "@Id_Language_trans", Value = Id_Language_trans },
                 new SqlParameter { ParameterName = "@Id_wordtype", Value = Id_wordtype },
                 new SqlParameter { ParameterName = "@Id_user", Value = Id_user },
                 new SqlParameter { ParameterName = "@sWord", Value = sWord },
                 new SqlParameter { ParameterName = "@sExample", Value = sExample },
                 new SqlParameter { ParameterName = "@sDefinition", Value = sDefinition },
                 new SqlParameter { ParameterName = "@sWordTrans", Value = sWordTrans },
            };
        this.Database.ExecuteSqlRaw(sql, parameters.ToArray());
    }


	public IQueryable<WordSearch> searchWord(int Id_Language, int Id_Language_trans, string sWord)
	{

		string sql = "EXECUTE getForceWords @word,@lang,@lang_trans";
		List<SqlParameter> parameters = new List<SqlParameter>
			{
				 new SqlParameter { ParameterName = "@word", Value = sWord },
				 new SqlParameter { ParameterName = "@lang", Value = Id_Language },
				 new SqlParameter { ParameterName = "@lang_trans", Value = Id_Language_trans },
			};
		return this.WordSearches.FromSqlRaw(sql, parameters.ToArray());
	}


	public IQueryable<WordSearch> preSearchWord(int Id_Language, int Id_Language_trans, string sWord)
	{

		string sql = "EXECUTE SearchWords @word,@lang,@lang_trans";
		List<SqlParameter> parameters = new List<SqlParameter>
			{
				 new SqlParameter { ParameterName = "@word", Value = sWord },
				 new SqlParameter { ParameterName = "@lang", Value = Id_Language },
				 new SqlParameter { ParameterName = "@lang_trans", Value = Id_Language_trans },
			};
		return this.WordSearches.FromSqlRaw(sql, parameters.ToArray());
	}


    public IQueryable<TblUser> getAllUser()
    {

        return this.TblUsers.FromSqlRaw("EXECUTE getAllUser");
    }


    public void UpgradeRoleUser(int idUser, string role)
    {

        string sql = "EXECUTE upgradeRoleUser @IdUser,@Role";
        List<SqlParameter> parameters = new List<SqlParameter>
            {
                 new SqlParameter { ParameterName = "@IdUser", Value = idUser },
                 new SqlParameter { ParameterName = "@Role", Value = role },
            };
        this.Database.ExecuteSqlRaw(sql, parameters.ToArray());
    }

    public void deleteUser(int idUser)
    {

        string sql = "EXECUTE deleteUser @IdUser";
        List<SqlParameter> parameters = new List<SqlParameter>
            {
                 new SqlParameter { ParameterName = "@IdUser", Value = idUser },
            };
        this.Database.ExecuteSqlRaw(sql, parameters.ToArray());
    }
}
