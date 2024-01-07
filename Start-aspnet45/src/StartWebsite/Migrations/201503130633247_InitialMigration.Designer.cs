using Khulnasoft.Data.Entity;
using Khulnasoft.Data.Entity.Metadata;
using Khulnasoft.Data.Entity.Relational.Migrations.Infrastructure;
using Start.Models;
using System;
using Start.Data;

namespace StartWebsite.Migrations
{
    [ContextType(typeof(StartContext))]
    public partial class InitialMigration : IMigrationMetadata
    {
        string IMigrationMetadata.MigrationId
        {
            get
            {
                return "201503130633247_InitialMigration";
            }
        }
        
        string IMigrationMetadata.ProductVersion
        {
            get
            {
                return "7.0.0-beta3-12166";
            }
        }
        
        IModel IMigrationMetadata.TargetModel
        {
            get
            {
                var builder = new BasicModelBuilder();
                
                builder.Entity("Khulnasoft.AspNet.Identity.IdentityRole", b =>
                    {
                        b.Property<string>("ConcurrencyStamp")
                            .ConcurrencyToken();
                        b.Property<string>("Id")
                            .GenerateValueOnAdd();
                        b.Property<string>("Name");
                        b.Property<string>("NormalizedName");
                        b.Key("Id");
                        b.ForRelational().Table("AspNetRoles");
                    });
                
                builder.Entity("Khulnasoft.AspNet.Identity.IdentityRoleClaim`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]", b =>
                    {
                        b.Property<string>("ClaimType");
                        b.Property<string>("ClaimValue");
                        b.Property<int>("Id")
                            .GenerateValueOnAdd();
                        b.Property<string>("RoleId");
                        b.Key("Id");
                        b.ForRelational().Table("AspNetRoleClaims");
                    });
                
                builder.Entity("Khulnasoft.AspNet.Identity.IdentityUserClaim`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]", b =>
                    {
                        b.Property<string>("ClaimType");
                        b.Property<string>("ClaimValue");
                        b.Property<int>("Id")
                            .GenerateValueOnAdd();
                        b.Property<string>("UserId");
                        b.Key("Id");
                        b.ForRelational().Table("AspNetUserClaims");
                    });
                
                builder.Entity("Khulnasoft.AspNet.Identity.IdentityUserLogin`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]", b =>
                    {
                        b.Property<string>("LoginProvider");
                        b.Property<string>("ProviderDisplayName");
                        b.Property<string>("ProviderKey");
                        b.Property<string>("UserId");
                        b.Key("LoginProvider", "ProviderKey");
                        b.ForRelational().Table("AspNetUserLogins");
                    });
                
                builder.Entity("Khulnasoft.AspNet.Identity.IdentityUserRole`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]", b =>
                    {
                        b.Property<string>("RoleId");
                        b.Property<string>("UserId");
                        b.Key("UserId", "RoleId");
                        b.ForRelational().Table("AspNetUserRoles");
                    });
                
                builder.Entity("Start.Models.ApplicationUser", b =>
                    {
                        b.Property<int>("AccessFailedCount");
                        b.Property<string>("ConcurrencyStamp")
                            .ConcurrencyToken();
                        b.Property<string>("Email");
                        b.Property<bool>("EmailConfirmed");
                        b.Property<string>("Id")
                            .GenerateValueOnAdd();
                        b.Property<bool>("LockoutEnabled");
                        b.Property<DateTimeOffset?>("LockoutEnd");
                        b.Property<string>("Name");
                        b.Property<string>("NormalizedEmail");
                        b.Property<string>("NormalizedUserName");
                        b.Property<string>("PasswordHash");
                        b.Property<string>("PhoneNumber");
                        b.Property<bool>("PhoneNumberConfirmed");
                        b.Property<string>("SecurityStamp");
                        b.Property<bool>("TwoFactorEnabled");
                        b.Property<string>("UserName");
                        b.Key("Id");
                        b.ForRelational().Table("AspNetUsers");
                    });
                
                builder.Entity("Start.Models.CartItem", b =>
                    {
                        b.Property<string>("CartId");
                        b.Property<int>("CartItemId")
                            .GenerateValueOnAdd();
                        b.Property<int>("Count");
                        b.Property<DateTime>("DateCreated");
                        b.Property<int>("ProductId");
                        b.Key("CartItemId");
                    });
                
                builder.Entity("Start.Models.Category", b =>
                    {
                        b.Property<int>("CategoryId")
                            .GenerateValueOnAdd();
                        b.Property<string>("Description");
                        b.Property<string>("Name");
                        b.Key("CategoryId");
                    });
                
                builder.Entity("Start.Models.Order", b =>
                    {
                        b.Property<string>("Address");
                        b.Property<string>("City");
                        b.Property<string>("Country");
                        b.Property<string>("Email");
                        b.Property<string>("Name");
                        b.Property<DateTime>("OrderDate");
                        b.Property<int>("OrderId")
                            .GenerateValueOnAdd();
                        b.Property<string>("Phone");
                        b.Property<string>("PostalCode");
                        b.Property<string>("State");
                        b.Property<decimal>("Total");
                        b.Property<string>("Username");
                        b.Key("OrderId");
                    });
                
                builder.Entity("Start.Models.OrderDetail", b =>
                    {
                        b.Property<int>("OrderDetailId")
                            .GenerateValueOnAdd();
                        b.Property<int>("OrderId");
                        b.Property<int>("ProductId");
                        b.Property<int>("Quantity");
                        b.Property<decimal>("UnitPrice");
                        b.Key("OrderDetailId");
                    });
                
                builder.Entity("Start.Models.Product", b =>
                    {
                        b.Property<int>("CategoryId");
                        b.Property<DateTime>("Created");
                        b.Property<decimal>("Price");
                        b.Property<string>("ProductArtUrl");
                        b.Property<int>("ProductId")
                            .GenerateValueOnAdd();
                        b.Property<decimal>("SalePrice");
                        b.Property<string>("Title");
                        b.Key("ProductId");
                    });
                
                builder.Entity("Start.Models.Raincheck", b =>
                    {
                        b.Property<string>("Name");
                        b.Property<int>("ProductId");
                        b.Property<int>("Quantity");
                        b.Property<int>("RaincheckId")
                            .GenerateValueOnAdd();
                        b.Property<double>("SalePrice");
                        b.Property<int>("StoreId");
                        b.Key("RaincheckId");
                    });
                
                builder.Entity("Start.Models.Store", b =>
                    {
                        b.Property<string>("Name");
                        b.Property<int>("StoreId")
                            .GenerateValueOnAdd();
                        b.Key("StoreId");
                    });
                
                builder.Entity("Khulnasoft.AspNet.Identity.IdentityRoleClaim`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]", b =>
                    {
                        b.ForeignKey("Khulnasoft.AspNet.Identity.IdentityRole", "RoleId");
                    });
                
                builder.Entity("Khulnasoft.AspNet.Identity.IdentityUserClaim`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]", b =>
                    {
                        b.ForeignKey("Start.Models.ApplicationUser", "UserId");
                    });
                
                builder.Entity("Khulnasoft.AspNet.Identity.IdentityUserLogin`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]", b =>
                    {
                        b.ForeignKey("Start.Models.ApplicationUser", "UserId");
                    });
                
                builder.Entity("Start.Models.CartItem", b =>
                    {
                        b.ForeignKey("Start.Models.Product", "ProductId");
                    });
                
                builder.Entity("Start.Models.OrderDetail", b =>
                    {
                        b.ForeignKey("Start.Models.Order", "OrderId");
                        b.ForeignKey("Start.Models.Product", "ProductId");
                    });
                
                builder.Entity("Start.Models.Product", b =>
                    {
                        b.ForeignKey("Start.Models.Category", "CategoryId");
                    });
                
                builder.Entity("Start.Models.Raincheck", b =>
                    {
                        b.ForeignKey("Start.Models.Store", "StoreId");
                        b.ForeignKey("Start.Models.Product", "ProductId");
                    });
                
                return builder.Model;
            }
        }
    }
}