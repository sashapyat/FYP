using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Core.Persistence.Entities;

namespace Core.Persistence.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //
            // book samples have been taken from following website: https://gutenberg.org/ebooks/bookshelf/
            //

            modelBuilder.Entity<Genres>().HasData(
                new Genres { GenreId = 1, GenreName = "Fantasy" },
                new Genres { GenreId = 2, GenreName = "Adventure" },
                new Genres { GenreId = 3, GenreName = "Romance" },
                new Genres { GenreId = 4, GenreName = "Mystery" },
                new Genres { GenreId = 5, GenreName = "Horror" },
                new Genres { GenreId = 6, GenreName = "Historical Fiction" },
                new Genres { GenreId = 7, GenreName = "Cooking" },
                new Genres { GenreId = 8, GenreName = "Classics" },
                new Genres { GenreId = 9, GenreName = "Health" },
                new Genres { GenreId = 10, GenreName = "Fiction" },
                new Genres { GenreId = 11, GenreName = "Literature" },
                new Genres { GenreId = 12, GenreName = "Crime" },
                new Genres { GenreId = 13, GenreName = "Thriller" },
                new Genres { GenreId = 14, GenreName = "Non-Fiction" },
                new Genres { GenreId = 15, GenreName = "Humour" }
            );

            modelBuilder.Entity<Authors>().HasData(
                new Authors { AuthorId = 1, AuthorFirstName = "M.R.", AuthorLastName = "James" },
                new Authors { AuthorId = 2, AuthorFirstName = "Bob", AuthorLastName = "Brown" },
                new Authors { AuthorId = 3, AuthorFirstName = "Wilkie", AuthorLastName = "Collins" },
                new Authors { AuthorId = 4, AuthorFirstName = "Joseph", AuthorLastName = "Conrad" },
                new Authors { AuthorId = 5, AuthorFirstName = "P. G.", AuthorLastName = "Wodehouse" },
                new Authors { AuthorId = 6, AuthorFirstName = "Charles", AuthorLastName = "Garvice" },
                new Authors { AuthorId = 7, AuthorFirstName = "Alexandre", AuthorLastName = "Dumas" },
                new Authors { AuthorId = 8, AuthorFirstName = "Victor", AuthorLastName = "Hugo" },
                new Authors { AuthorId = 9, AuthorFirstName = "Jack", AuthorLastName = "London" }
            );

            modelBuilder.Entity<GenresInBook>().HasData(
                new GenresInBook { GenresInBookId = 1, BookId = 1, GenreId = 1 },
                new GenresInBook { GenresInBookId = 2, BookId = 1, GenreId = 10 },
                new GenresInBook { GenresInBookId = 3, BookId = 1, GenreId = 5 },
                new GenresInBook { GenresInBookId = 4, BookId = 2, GenreId = 14 },
                new GenresInBook { GenresInBookId = 5, BookId = 2, GenreId = 7 },
                new GenresInBook { GenresInBookId = 6, BookId = 3, GenreId = 10 },
                new GenresInBook { GenresInBookId = 7, BookId = 3, GenreId = 6 },
                new GenresInBook { GenresInBookId = 8, BookId = 3, GenreId = 4 },
                new GenresInBook { GenresInBookId = 9, BookId = 3, GenreId = 13 },
                new GenresInBook { GenresInBookId = 10, BookId = 3, GenreId = 11 },
                new GenresInBook { GenresInBookId = 11, BookId = 3, GenreId = 8 },
                new GenresInBook { GenresInBookId = 12, BookId = 4, GenreId = 8 },
                new GenresInBook { GenresInBookId = 13, BookId = 4, GenreId = 11 },
                new GenresInBook { GenresInBookId = 14, BookId = 4, GenreId = 4 },
                new GenresInBook { GenresInBookId = 15, BookId = 4, GenreId = 12 },
                new GenresInBook { GenresInBookId = 16, BookId = 4, GenreId = 13 },
                new GenresInBook { GenresInBookId = 17, BookId = 5, GenreId = 15 },
                new GenresInBook { GenresInBookId = 18, BookId = 5, GenreId = 11 },
                new GenresInBook { GenresInBookId = 19, BookId = 5, GenreId = 10 },
                new GenresInBook { GenresInBookId = 20, BookId = 5, GenreId = 6 },
                new GenresInBook { GenresInBookId = 21, BookId = 6, GenreId = 3 },
                new GenresInBook { GenresInBookId = 22, BookId = 6, GenreId = 11 },
                new GenresInBook { GenresInBookId = 23, BookId = 6, GenreId = 10 },
                new GenresInBook { GenresInBookId = 24, BookId = 7, GenreId = 10 },
                new GenresInBook { GenresInBookId = 25, BookId = 7, GenreId = 8 },
                new GenresInBook { GenresInBookId = 26, BookId = 7, GenreId = 6 },
                new GenresInBook { GenresInBookId = 27, BookId = 7, GenreId = 2 },
                new GenresInBook { GenresInBookId = 28, BookId = 7, GenreId = 11 },
                new GenresInBook { GenresInBookId = 29, BookId = 8, GenreId = 11 },
                new GenresInBook { GenresInBookId = 30, BookId = 8, GenreId = 8 },
                new GenresInBook { GenresInBookId = 31, BookId = 8, GenreId = 6 },
                new GenresInBook { GenresInBookId = 32, BookId = 8, GenreId = 10 },
                new GenresInBook { GenresInBookId = 33, BookId = 8, GenreId = 3 },
                new GenresInBook { GenresInBookId = 34, BookId = 9, GenreId = 8 },
                new GenresInBook { GenresInBookId = 35, BookId = 9, GenreId = 10 },
                new GenresInBook { GenresInBookId = 36, BookId = 9, GenreId = 2 },
                new GenresInBook { GenresInBookId = 37, BookId = 9, GenreId = 6 },
                new GenresInBook { GenresInBookId = 38, BookId = 9, GenreId = 11 },
                new GenresInBook { GenresInBookId = 39, BookId = 10, GenreId = 10 },
                new GenresInBook { GenresInBookId = 40, BookId = 10, GenreId = 8 },
                new GenresInBook { GenresInBookId = 41, BookId = 10, GenreId = 6 },
                new GenresInBook { GenresInBookId = 42, BookId = 10, GenreId = 2 },
                new GenresInBook { GenresInBookId = 43, BookId = 10, GenreId = 11 }


            );

            modelBuilder.Entity<Books>(document =>
            {
                document.Property(d => d.PublicationYear).HasColumnType("DATE");
                document.Property(d => d.Summary).HasColumnType("TEXT");
            });
            modelBuilder.Entity<Books>().HasData(
                new Books { BookId = 1, BookTitle = "Ghost Stories of an Antiquary", PublicationYear = new DateTime(1904, 1, 1), AuthorId = 1, Summary = "Eight classics by a great Edwardian scholar and storyteller. \"Number Thirteen,\" \"The Mezzotint,\" \"Canon Alberic's Scrapbook,\" and more. Renowned for their wit, erudition and suspense, these stories are each masterfully constructed and represent a high achievement in the ghost genre. We are delighted to publish this classic book as part of our extensive Classic Library collection. Many of the books in our collection have been out of print for decades, and therefore have not been accessible to the general public. The aim of our publishing program is to facilitate rapid access to this vast reservoir of literature, and our view is that this is a significant literary work, which deserves to be brought back into print after many decades. The contents of the vast majority of titles in the Classic Library have been scanned from the original works. To ensure a high quality product, each title has been meticulously hand curated by our staff. Our philosophy has been guided by a desire to provide the reader with a book that is as close as possible to ownership of the original work. We hope that you will enjoy this wonderful classic work, and that for you it becomes an enriching experience.", BookCover = "GhostStoriesOfAnAntiquary.jpg", BookPdfUrl = "GhostStoriesOfAnAntiquary.pdf" },
                new Books { BookId = 2, BookTitle = "The Complete Book of Cheese", PublicationYear = new DateTime(1955, 1, 1), AuthorId = 2, Summary = "1. I Remember Cheese 2. The Big Cheese 3. Foreign Greats 4. Native Americans 5. Sixty-five Sizzling Rabbits 6. The Fondue 7. Soufflés, Puffs and Ramekins 8. Pizzas, Blintzes, Pastes and Cheese Cake 9. Au Gratin, Soups, Salads and Sauces 10. Appetizers, Crackers, Sandwiches, Savories, Snacks, Spreads and Toasts 11. \"Fit for Drink\" 12. Lazy Lou APPENDIX—The A-B-Z of Cheese", BookCover = "TheCompleteBookOfCheese.jpg", BookPdfUrl = "TheCompleteBookOfCheese.pdf" },
                new Books { BookId = 3, BookTitle = "The Woman in White", PublicationYear = new DateTime(1859, 1, 1), AuthorId = 3, Summary = "'In one moment, every drop of blood in my body was brought to a stop... There, as if it had that moment sprung out of the earth, stood the figure of a solitary Woman, dressed from head to foot in white' The Woman in White famously opens with Walter Hartright's eerie encounter on a moonlit London road. Engaged as a drawing master to the beautiful Laura Fairlie, Walter becomes embroiled in the sinister intrigues of Sir Percival Glyde and his 'charming' friend Count Fosco, who has a taste for white mice, vanilla bonbons, and poison. Pursuing questions of identity and insanity along the paths and corridors of English country houses and the madhouse, The Woman in White is the first and most influential of the Victorian genre that combined Gothic horror with psychological realism. Matthew Sweet's introduction explores the phenomenon of Victorian 'sensation' fiction, and discusses Wilkie Collins's biographical and societal influences. Included in this edition are appendices on theatrical adaptations of the novel and its serialisation history.", BookCover = "TheWomanInWhite.jpg", BookPdfUrl = "TheWomanInWhite.pdf" },
                new Books { BookId = 4, BookTitle = "The Secret Agent: A Simple Tale", PublicationYear = new DateTime(1907, 1, 1), AuthorId = 4, Summary = "In a rough-and-ready shop in Soho in Victorian London, Mr. Verloc lives a humble existence sustaining himself, his wife, her infirm mother, and her disabled brother, Stevie. But all is not as it seems with Mr. Verloc, a secret agent in the employ of a foreign government. When a plot to bomb Greenwich Observatory falls apart, Verloc’s identity is exposed, and those closest to him must bear the burden of his failures. Harkening back to the writings of Charles Dickens, Joseph Conrad’s early masterpiece effectively explores themes of subversion, politics, and crime.)", BookCover = "TheSimpleAgentASimpleTale.jpg", BookPdfUrl = "TheSimpleAgentASimpleTale.pdf" },
                new Books { BookId = 5, BookTitle = "Right Ho, Jeeves", PublicationYear = new DateTime(1934, 1, 1), AuthorId = 5, Summary = "Gussie Fink-Nottle has locked himself away in the country studying newts ever since he came into his estate. So it is a surprise when Bertie hears that Gussie is not only in London, but he is there to woo Madeline Bassett! At odds with Jeeves over the decorum of a white jacket, Bertie decides to take on Gussie's problem himself. Off to Brinkley Court, Bertie must deal with the prize-giving at Market Snodsbury Grammar School, the broken engagement of his cousin Angela, and the resignation of Anatole, his aunt's genius chef. Will Jeeves be able to sort out the mess?", BookCover = "RightHoJeeves.jpg", BookPdfUrl = "RightHoJeeves.pdf" },
                new Books { BookId = 6, BookTitle = "Only a Girl's Love", PublicationYear = new DateTime(1979, 1, 1), AuthorId = 6, Summary = "It is a warm evening in early Summer; the sun is setting behind a long range of fir and yew-clad hills, at the feet of which twists in and out, as it follows their curves, a placid, peaceful river. Opposite these hills, and running beside the river, are long-stretching meadows, brilliantly green with fresh-springing grass, and gorgeously yellow with newly-opened buttercups. Above, the sunset sky gleams and glows with fiery red and rich deep chromes. And London is almost within sight. It is a beautiful scene, such as one sees only in this England of ours--a scene that defies poet and painter. At this very moment it is defying one of the latter genus; for in a room of a low-browed, thatched-roofed cottage which stood on the margin of the meadow, James Etheridge sat beside his easel, his eyes fixed on the picture framed in the open window, his brush and mahl-stick drooping in his idle hand", BookCover = "OnlyAGirlsLove.jpg", BookPdfUrl = "OnlyAGirlsLove.pdf" },
                new Books { BookId = 7, BookTitle = "Twenty Years After", PublicationYear = new DateTime(1845, 1, 1), AuthorId = 7, Summary = "Twenty Years After (1845), the sequel to The Three Musketeers, is a supreme creation of suspense and heroic adventure. Two decades have passed since the musketeers triumphed over Cardinal Richelieu and Milady. Time has weakened their resolve, and dispersed their loyalties. But treasons and stratagems still cry out for justice: civil war endangers the throne of France, while in England Cromwell threatens to send Charles I to the scaffold. Dumas brings his immortal quartet out of retirement to cross swords with time, the malevolence of men, and the forces of history. But their greatest test is a titanic struggle with the son of Milady, who wears the face of Evil.", BookCover = "TwentyYearsAfter.jpg", BookPdfUrl = "TwentyYearsAfter.pdf" },
                new Books { BookId = 8, BookTitle = "Les Misérables", PublicationYear = new DateTime(1862, 1, 1), AuthorId = 8, Summary = "Victor Hugo's tale of injustice, heroism and love follows the fortunes of Jean Valjean, an escaped convict determined to put his criminal past behind him. But his attempts to become a respected member of the community are constantly put under threat: by his own conscience, when, owing to a case of mistaken identity, another man is arrested in his place; and by the relentless investigations of the dogged Inspector Javert. It is not simply for himself that Valjean must stay free, however, for he has sworn to protect the baby daughter of Fantine, driven to prostitution by poverty.", BookCover = "LesMiserables.jpg", BookPdfUrl = "LesMiserables.pdf" },
                new Books { BookId = 9, BookTitle = "The Call of the Wild", PublicationYear = new DateTime(1903, 1, 1), AuthorId = 9, Summary = "First published in 1903, The Call of the Wild is regarded as Jack London's masterpiece. Based on London's experiences as a gold prospector in the Canadian wilderness and his ideas about nature and the struggle for existence, The Call of the Wild is a tale about unbreakable spirit and the fight for survival in the frozen Alaskan Klondike.", BookCover = "TheCallOfTheWild.jpg", BookPdfUrl = "TheCallOfTheWild.pdf" },
                new Books { BookId = 10, BookTitle = "The Three Musketeers", PublicationYear = new DateTime(1844, 1, 1), AuthorId = 7, Summary = "Alexandre Dumas’s most famous tale— and possibly the most famous historical novel of all time— in a handsome hardcover volume. This swashbuckling epic of chivalry, honor, and derring-do, set in France during the 1620s, is richly populated with romantic heroes, unattainable heroines, kings, queens, cavaliers, and criminals in a whirl of adventure, espionage, conspiracy, murder, vengeance, love, scandal, and suspense. Dumas transforms minor historical figures into larger- than-life characters: the Comte d’Artagnan, an impetuous young man in pursuit of glory; the beguilingly evil seductress “Milady”; the powerful and devious Cardinal Richelieu; the weak King Louis XIII and his unhappy queen—and, of course, the three musketeers themselves, Athos, Porthos, and Aramis, whose motto “all for one, one for all” has come to epitomize devoted friendship. With a plot that delivers stolen diamonds, masked balls, purloined letters, and, of course, great bouts of swordplay, The Three Musketeers is eternally entertaining.", BookCover = "TheThreeMusketeers.jpg", BookPdfUrl = "TheThreeMusketeers.pdf" }

            );

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "Admin", Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "1" },
                new IdentityRole { Id = "RegisteredUser", Name = "RegisteredUser", NormalizedName = "REGISTEREDUSER", ConcurrencyStamp = "2" },
                new IdentityRole { Id = "PaidMember", Name = "PaidMember", NormalizedName = "PAIDMEMBER", ConcurrencyStamp = "3" }
            );

            


        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Authors> Authors { get; set; }
        public DbSet<Genres> Genres { get; set; }
        public DbSet<GenresInBook> GenresInBook { get; set; }
        public DbSet<BookLists> BookLists { get; set; }
        public DbSet<UserBookLists> UserBookLists { get; set; }
        public DbSet<Membership> Membership { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<DigitalLicence> DigitalLicence { get; set; }

    }
}