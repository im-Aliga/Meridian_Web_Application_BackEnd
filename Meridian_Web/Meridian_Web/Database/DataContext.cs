using BackEndFinalProject.Database.Models;
using Meridian_Web.Database.Models;
using Meridian_Web.Database.Models.Common;
using Meridian_Web.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Meridian_Web.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketProduct> BasketProducts { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogFile> BlogFiles { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<BlogAndBlogTag> BlogAndBlogTags { get; set; }
        public DbSet<BlogAndBlogCategory> BlogAndBlogCategories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactInformation> ContactInformations { get; set; }
        public DbSet<Discont> Disconts { get; set; }
        public DbSet<FeedBack> FeedBacks { get; set; }
        public DbSet<GlobalOffer> GlobalOffers { get; set; }
        public DbSet<Navbar> Navbars { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductCatagory> ProductCatagories { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductDiscont> ProductDisconts { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SubNavbar> SubNavbars { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserActivation> UserActivations { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<WishlistProduct> WishlistProducts { get; set; }
        public DbSet<IdentityUserLogin<Guid>> IdentityUserLogins { get; set; }


















        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly<Program>();
        }



        #region Auditible
        public override int SaveChanges()
        {
            AutoAudit();


            return base.SaveChanges(); //bu hemise olmalidirki base save changes islerini gorsun..
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            AutoAudit();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            AutoAudit();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AutoAudit();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AutoAudit()
        {
            foreach (var entity in ChangeTracker.Entries())
            {
                if (entity.Entity is not IAuditable auditable) // burada is not un diger bir ozelliyinden istifade edirik
                                                               //yeni entity IAuditabli implement eleyibse ve casting ede bilirse onu IAuditabledan casting edir
                {
                    continue;
                }
                /* var auditable = (IAuditable)entity;*/ // casting for acces IAuditable properties on entity

                DateTime currentTime = DateTime.Now; //for same time 

                if (entity.State == EntityState.Added) // for checking entity's state added
                {
                    auditable.CreatedAt = currentTime;
                    auditable.UpdatedAt = currentTime;
                }
                else if (entity.State == EntityState.Modified) // for checking entity's state modified
                {
                    auditable.UpdatedAt = currentTime;

                }
            }
        } 
        #endregion
    }
}
