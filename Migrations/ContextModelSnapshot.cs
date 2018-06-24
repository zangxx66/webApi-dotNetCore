﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPI.Models;

namespace WebAPI.Migrations {
    [DbContext (typeof (Context))]
    partial class ContextModelSnapshot : ModelSnapshot {
        protected override void BuildModel (ModelBuilder modelBuilder) {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation ("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation ("Relational:MaxIdentifierLength", 128)
                .HasAnnotation ("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity ("WebAPI.Models.Article", b => {
                b.Property<Guid> ("Id")
                    .ValueGeneratedOnAdd ();

                b.Property<Guid?> ("AnthorId");

                b.Property<Guid?> ("CategoryId");

                b.Property<string> ("Context");

                b.Property<DateTime> ("CreateDate");

                b.Property<string> ("Summary");

                b.Property<string> ("Title");

                b.HasKey ("Id");

                b.HasIndex ("AnthorId");

                b.HasIndex ("CategoryId");

                b.ToTable ("Article");
            });

            modelBuilder.Entity ("WebAPI.Models.Category", b => {
                b.Property<Guid> ("Id")
                    .ValueGeneratedOnAdd ();

                b.Property<string> ("Name");

                b.Property<int> ("Sort");

                b.HasKey ("Id");

                b.ToTable ("Category");
            });

            modelBuilder.Entity ("WebAPI.Models.Comment", b => {
                b.Property<Guid> ("Id")
                    .ValueGeneratedOnAdd ();

                b.Property<Guid?> ("ArticleId");

                b.Property<string> ("Context");

                b.Property<DateTime> ("CreateDate");

                b.Property<string> ("Email");

                b.Property<string> ("NickName");

                b.Property<Guid?> ("parentCommentId");

                b.HasKey ("Id");

                b.HasIndex ("ArticleId");

                b.HasIndex ("parentCommentId");

                b.ToTable ("Comment");
            });

            modelBuilder.Entity ("WebAPI.Models.Logs", b => {
                b.Property<Guid> ("Id")
                    .ValueGeneratedOnAdd ();

                b.Property<DateTime> ("CreateDate");

                b.Property<string> ("Expcetion");

                b.HasKey ("Id");

                b.ToTable ("Logs");
            });

            modelBuilder.Entity ("WebAPI.Models.User", b => {
                b.Property<Guid> ("Id")
                    .ValueGeneratedOnAdd ();

                b.Property<string> ("Avatar");

                b.Property<string> ("Description");

                b.Property<string> ("Email");

                b.Property<string> ("Git");

                b.Property<DateTime> ("LastDate");

                b.Property<string> ("LastIp");

                b.Property<string> ("NickName");

                b.Property<string> ("Password");

                b.Property<int> ("QQ");

                b.Property<DateTime> ("RegDate");

                b.Property<string> ("RegIp");

                b.Property<int> ("Role");

                b.Property<int> ("Sex");

                b.Property<string> ("Twitter");

                b.Property<string> ("UserName");

                b.Property<string> ("Weibo");

                b.HasKey ("Id");

                b.ToTable ("User");
            });

            modelBuilder.Entity ("WebAPI.Models.Article", b => {
                b.HasOne ("WebAPI.Models.User", "Anthor")
                    .WithMany ()
                    .HasForeignKey ("AnthorId");

                b.HasOne ("WebAPI.Models.Category", "Category")
                    .WithMany ()
                    .HasForeignKey ("CategoryId");
            });

            modelBuilder.Entity ("WebAPI.Models.Comment", b => {
                b.HasOne ("WebAPI.Models.Article")
                    .WithMany ("Comment")
                    .HasForeignKey ("ArticleId");

                b.HasOne ("WebAPI.Models.Comment", "parentComment")
                    .WithMany ()
                    .HasForeignKey ("parentCommentId");
            });

#pragma warning restore 612, 618
        }
    }
}