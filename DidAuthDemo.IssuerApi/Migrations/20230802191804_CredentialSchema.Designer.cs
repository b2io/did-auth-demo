﻿// <auto-generated />
using System;
using DidAuthDemo.IssuerApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DidAuthDemo.IssuerApi.Migrations
{
    [DbContext(typeof(IssuerDbContext))]
    [Migration("20230802191804_CredentialSchema")]
    partial class CredentialSchema
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DidAuthDemo.IssuerApi.Data.Entities.Auth", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Challenge")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("challenge");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("DidOwner")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("did_owner");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_auths");

                    b.ToTable("auths", (string)null);
                });

            modelBuilder.Entity("DidAuthDemo.IssuerApi.Data.Entities.CredentialSchema", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("OwnerDid")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("owner_did");

                    b.Property<string>("SchemaDefinition")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("schema_definition");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_credential_schemas");

                    b.HasIndex(new[] { "OwnerDid" }, "Index_OwnerDid")
                        .HasDatabaseName("ix_credential_schemas_owner_did");

                    b.ToTable("credential_schemas", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
