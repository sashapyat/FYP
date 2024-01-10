using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesGenresAuthorsBooksGenresInBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorLastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicationYear = table.Column<DateTime>(type: "DATE", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    Summary = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenresInBook",
                columns: table => new
                {
                    GenresInBookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenresInBook", x => x.GenresInBookId);
                    table.ForeignKey(
                        name: "FK_GenresInBook_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenresInBook_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "AuthorFirstName", "AuthorLastName" },
                values: new object[,]
                {
                    { 1, "J.K", "Rowling" },
                    { 2, "J.R.R", "Tolkien" },
                    { 3, "Jane", "Austen" },
                    { 4, "John", "Green" },
                    { 5, "Nicholas", "Sparks" },
                    { 6, "Rainbow", "Rowell" },
                    { 7, "Agatha", "Christie" },
                    { 8, "Gillian", "Flynn" },
                    { 9, "Dan", "Brown" },
                    { 10, "Holly", "Jackson" },
                    { 11, "Bill", "Bryson" },
                    { 12, "Jon", "Krakauer" },
                    { 13, "Stephen", "King" },
                    { 14, "Shirly", "Jackson" },
                    { 15, "Bram", "Stoker" },
                    { 16, "Mathew", "Walker" },
                    { 17, "Michael", "Pollan" },
                    { 18, "Irma S.", "Rombauer" },
                    { 19, "Julia", "Child" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "GenreId", "GenreName" },
                values: new object[,]
                {
                    { 1, "Fantasy" },
                    { 2, "Adventure" },
                    { 3, "Romance" },
                    { 4, "Mystery" },
                    { 5, "Horror" },
                    { 6, "Historical Fiction" },
                    { 7, "Cooking" },
                    { 8, "History" },
                    { 9, "Health" },
                    { 10, "Travel" },
                    { 11, "Fiction" },
                    { 12, "Young Adult" },
                    { 13, "Middle Grade" },
                    { 14, "Adult" },
                    { 15, "Crime" },
                    { 16, "Thriller" },
                    { 17, "Non-Fiction" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "AuthorId", "BookTitle", "PublicationYear", "Summary" },
                values: new object[,]
                {
                    { 1, 1, "Harry Potter and the Philosopher’s Stone (Harry Potter #1)", new DateTime(1997, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter thinks he is an ordinary boy - until he is rescued by an owl, taken to Hogwarts School of Witchcraft and Wizardry, learns to play Quidditch and does battle in a deadly duel. The Reason ... HARRY POTTER IS A WIZARD!" },
                    { 2, 1, "Harry Potter and the Chamber of Secrets (Harry Potter #2)", new DateTime(1998, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ever since Harry Potter had come home for the summer, the Dursleys had been so mean and hideous that all Harry wanted was to get back to the Hogwarts School for Witchcraft and Wizardry. But just as he’s packing his bags, Harry receives a warning from a strange impish creature who says that if Harry returns to Hogwarts, disaster will strike. And strike it does. For in Harry’s second year at Hogwarts, fresh torments and horrors arise, including an outrageously stuck-up new professor and a spirit who haunts the girls’ bathroom. But then the real trouble begins – someone is turning Hogwarts students to stone. Could it be Draco Malfoy, a more poisonous rival than ever? Could it possibly be Hagrid, whose mysterious past is finally told? Or could it be the one everyone at Hogwarts most suspects… Harry Potter himself!" },
                    { 3, 1, "Harry Potter and the Prisoner of Azkaban (Harry Potter #3)", new DateTime(1999, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter, along with his best friends, Ron and Hermione, is about to start his third year at Hogwarts School of Witchcraft and Wizardry. Harry can't wait to get back to school after the summer holidays. (Who wouldn't if they lived with the horrible Dursleys?) But when Harry gets to Hogwarts, the atmosphere is tense. There's an escaped mass murderer on the loose, and the sinister prison guards of Azkaban have been called in to guard the school..." },
                    { 4, 1, "Harry Potter and the Goblet of Fire (Harry Potter #4)", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "It is the summer holidays and soon Harry Potter will be starting his fourth year at Hogwarts School of Witchcraft and Wizardry. Harry is counting the days: there are new spells to be learnt, more Quidditch to be played, and Hogwarts castle to continue exploring. But Harry needs to be careful - there are unexpected dangers lurking..." },
                    { 5, 1, "Harry Potter and the Order of the Phoenix (Harry Potter #5)", new DateTime(2003, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter is about to start his fifth year at Hogwarts School of Witchcraft and Wizardry. Unlike most schoolboys, Harry never enjoys his summer holidays, but this summer is even worse than usual. The Dursleys, of course, are making his life a misery, but even his best friends, Ron and Hermione, seem to be neglecting him. Harry has had enough. He is beginning to think he must do something, anything, to change his situation, when the summer holidays come to an end in a very dramatic fashion. What Harry is about to discover in his new year at Hogwarts will turn his world upside down..." },
                    { 6, 1, "Harry Potter and the Half-Blood Prince (Harry Potter #6)", new DateTime(2005, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "It is the middle of the summer, but there is an unseasonal mist pressing against the windowpanes. Harry Potter is waiting nervously in his bedroom at the Dursleys' house in Privet Drive for a visit from Professor Dumbledore himself. One of the last times he saw the Headmaster was in a fierce one-to-one duel with Lord Voldemort, and Harry can't quite believe that Professor Dumbledore will actually appear at the Dursleys' of all places. Why is the Professor coming to visit him now? What is it that cannot wait until Harry returns to Hogwarts in a few weeks' time? Harry's sixth year at Hogwarts has already got off to an unusual start, as the worlds of Muggle and magic start to intertwine..." },
                    { 7, 1, "Harry Potter and the Deathly Hallows (Harry Potter #7)", new DateTime(2007, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry has been burdened with a dark, dangerous and seemingly impossible task: that of locating and destroying Voldemort's remaining Horcruxes. Never has Harry felt so alone, or faced a future so full of shadows. But Harry must somehow find within himself the strength to complete the task he has been given. He must leave the warmth, safety and companionship of The Burrow and follow without fear or hesitation the inexorable path laid out for him... In this final, seventh installment of the Harry Potter series, J.K. Rowling unveils in spectacular fashion the answers to the many questions that have been so eagerly awaited." },
                    { 8, 1, "Harry Potter and the Cursed Child: Parts One and Two (Harry Potter #8)", new DateTime(2016, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The eighth story, nineteen years later... It was always difficult being Harry Potter, and it isn't much easier now that he is an overworked employee of the Ministry of Magic, a husband, and a father of three school-age children. While Harry grapples with a past that refuses to stay where it belongs, his youngest son, Albus, must struggle with the weight of a family legacy he never wanted. As past and present fuse ominously, both father and son learn the uncomfortable truth: sometimes, darkness comes from unexpected places. Based on an original new story by J.K. Rowling, Jack Thorne, and John Tiffany, a new play by Jack Thorne, \"Harry Potter and the Cursed Child\" is the complete and official playscript of the original, award-winning West End production. This updated edition includes the final dialogue and stage directions, a conversation piece between director John Tiffany and playwright Jack Thorne, the Potter family tree, and a timeline of events in the wizarding world leading up to \"Harry Potter and the Cursed Child.\"" },
                    { 9, 2, "The Hobbit (The Lord of the Rings #0)", new DateTime(1937, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "n a hole in the ground there lived a hobbit. Not a nasty, dirty, wet hole, filled with the ends of worms and an oozy smell, nor yet a dry, bare, sandy hole with nothing in it to sit down on or to eat: it was a hobbit-hole, and that means comfort.\r\nWritten for J.R.R. Tolkien’s own children, The Hobbit met with instant critical acclaim when it was first published in 1937. Now recognized as a timeless classic, this introduction to the hobbit Bilbo Baggins, the wizard Gandalf, Gollum, and the spectacular world of Middle-earth recounts of the adventures of a reluctant hero, a powerful and dangerous ring, and the cruel dragon Smaug the Magnificent. The text in this 372-page paperback edition is based on that first published in Great Britain by Collins Modern Classics (1998), and includes a note on the text by Douglas A. Anderson (2001)." },
                    { 10, 2, "The Fellowship of the Ring (The Lord of the Rings #1)", new DateTime(1954, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "One Ring to rule them all, One Ring to find them, One Ring to bring them all and in the darkness bind them. In ancient times the Rings of Power were crafted by the Elven-smiths, and Sauron, the Dark Lord, forged the One Ring, filling it with his own power so that he could rule all others. But the One Ring was taken from him, and though he sought it throughout Middle-earth, it remained lost to him. After many ages it fell into the hands of Bilbo Baggins, as told in The Hobbit. In a sleepy village in the Shire, young Frodo Baggins finds himself faced with an immense task, as his elderly cousin Bilbo entrusts the Ring to his care. Frodo must leave his home and make a perilous journey across Middle-earth to the Cracks of Doom, there to destroy the Ring and foil the Dark Lord in his evil purpose." },
                    { 11, 2, "The Two Towers (The Lord of the Rings #2)", new DateTime(1954, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Begin your journey into Middle-earth. The inspiration for the upcoming original series on Prime Video, The Lord of the Rings: The Rings of Power. The Two Towers is the second part of J.R.R. Tolkien’s epic adventure The Lord of the Rings. One Ring to rule them all, One Ring to find them, One Ring to bring them all and in the darkness bind them. Frodo and his Companions of the Ring have been beset by danger during their quest to prevent the Ruling Ring from falling into the hands of the Dark Lord by destroying it in the Cracks of Doom. They have lost the wizard, Gandalf, in a battle in the Mines of Moria. And Boromir, seduced by the power of the Ring, tried to seize it by force. While Frodo and Sam made their escape, the rest of the company was attacked by Orcs. Now they continue the journey alone down the great River Anduin—alone, that is, save for the mysterious creeping figure that follows wherever they go." },
                    { 12, 2, "The Return of the King (The Lord of the Rings #3)", new DateTime(1955, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Begin your journey into Middle-earth. The inspiration for the upcoming original series on Prime Video, The Lord of the Rings: The Rings of Power. The Return of the King is the third part of J.R.R. Tolkien’s epic adventure The Lord of the Rings. One Ring to rule them all, One Ring to find them, One Ring to bring them all and in the darkness bind them. The Dark Lord has risen, and as he unleashes hordes of Orcs to conquer all Middle-earth, Frodo and Sam struggle deep into his realm in Mordor. To defeat Sauron, the One Ring must be destroyed in the fires of Mount Doom. But the way is impossibly hard, and Frodo is weakening. The Ring corrupts all who bear it and Frodo’s time is running out. Will Sam and Frodo succeed, or will the Dark Lord rule Middle-earth once more?" },
                    { 13, 3, "Pride and Prejudice", new DateTime(1813, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Since its immediate success in 1813, Pride and Prejudice has remained one of the most popular novels in the English language. Jane Austen called this brilliant work \"her own darling child\" and its vivacious heroine, Elizabeth Bennet, \"as delightful a creature as ever appeared in print.\" The romantic clash between the opinionated Elizabeth and her proud beau, Mr. Darcy, is a splendid performance of civilized sparring. And Jane Austen's radiant wit sparkles as her characters dance a delicate quadrille of flirtation and intrigue, making this book the most superb comedy of manners of Regency England." },
                    { 14, 4, "The Fault in Our Stars", new DateTime(2012, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Despite the tumor-shrinking medical miracle that has bought her a few years, Hazel has never been anything but terminal, her final chapter inscribed upon diagnosis. But when a gorgeous plot twist named Augustus Waters suddenly appears at Cancer Kid Support Group, Hazel's story is about to be completely rewritten. Insightful, bold, irreverent, and raw, The Fault in Our Stars is award-winning author John Green's most ambitious and heartbreaking work yet, brilliantly exploring the funny, thrilling, and tragic business of being alive and in love." },
                    { 15, 4, "Looking for Alaska", new DateTime(2005, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Before. Miles “Pudge” Halter is done with his safe life at home. His whole life has been one big non-event, and his obsession with famous last words has only made him crave “the Great Perhaps” even more (Francois Rabelais, poet). He heads off to the sometimes crazy and anything-but-boring world of Culver Creek Boarding School, and his life becomes the opposite of safe. Because down the hall is Alaska Young. The gorgeous, clever, funny, sexy, self-destructive, screwed up, and utterly fascinating Alaska Young. She is an event unto herself. She pulls Pudge into her world, launches him into the Great Perhaps, and steals his heart. Then. . . . After. Nothing is ever the same." },
                    { 16, 5, "The Notebook", new DateTime(1996, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Set amid the austere beauty of the North Carolina coast begins the story of Noah Calhoun, a rural Southerner recently returned from the Second World War. Noah is restoring a plantation home to its former glory, and he is haunted by images of the beautiful girl he met fourteen years earlier, a girl he loved like no other. Unable to find her, yet unwilling to forget the summer they spent together, Noah is content to live with only memories...until she unexpectedly returns to his town to see him once again. Like a puzzle within a puzzle, the story of Noah and Allie is just the beginning. As it unfolds, their tale miraculously becomes something different, with much higher stakes. The result is a deeply moving portrait of love itself, the tender moments and the fundamental changes that affect us all. It is a story of miracles and emotions that will stay with you forever." },
                    { 17, 6, "Fangirl", new DateTime(2013, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cath is a Simon Snow fan. Okay, the whole world is a Simon Snow fan.... But for Cath, being a fan is her life—and she's really good at it. She and her twin, Wren, ensconced themselves in the Simon Snow series when they were just kids; it's what got them through their mother leaving. Reading. Rereading. Hanging out in Simon Snow forums, writing Simon Snow fanfiction, dressing up like the characters for every movie premiere. Cath's sister has mostly grown away from fandom, but Cath can't let go. She doesn't want to. Now that they're going to college, Wren has told Cath she doesn't want to be roommates. Cath is on her own, completely outside of her comfort zone. She's got a surly roommate with a charming, always-around boyfriend; a fiction-writing professor who thinks fanfiction is the end of the civilized world; a handsome classmate who only wants to talk about words... and she can't stop worrying about her dad, who's loving and fragile and has never really been alone. For Cath, the question is: Can she do this? Can she make it without Wren holding her hand? Is she ready to start living her own life? And does she even want to move on if it means leaving Simon Snow behind?" },
                    { 18, 7, "And Then There Were None", new DateTime(1939, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "First, there were ten—a curious assortment of strangers summoned as weekend guests to a little private island off the coast of Devon. Their host, an eccentric millionaire unknown to all of them, is nowhere to be found. All that the guests have in common is a wicked past they're unwilling to reveal—and a secret that will seal their fate. For each has been marked for murder. A famous nursery rhyme is framed and hung in every room of the mansion: \"Ten little boys went out to dine; One choked his little self and then there were nine. Nine little boys sat up very late; One overslept himself and then there were eight. Eight little boys traveling in Devon; One said he'd stay there then there were seven. Seven little boys chopping up sticks; One chopped himself in half and then there were six. Six little boys playing with a hive; A bumblebee stung one and then there were five. Five little boys going in for law; One got in Chancery and then there were four. Four little boys going out to sea; A red herring swallowed one and then there were three. Three little boys walking in the zoo; A big bear hugged one and then there were two. Two little boys sitting in the sun; One got frizzled up and then there was one. One little boy left all alone; He went out and hanged himself and then there were none.\" When they realize that murders are occurring as described in the rhyme, terror mounts. One by one they fall prey. Before the weekend is out, there will be none. Who has choreographed this dastardly scheme? And who will be left to tell the tale? Only the dead are above suspicion." },
                    { 19, 8, "Gone Girl", new DateTime(2012, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Who are you? What have we done to each other? These are the questions Nick Dunne finds himself asking on the morning of his fifth wedding anniversary when his wife Amy suddenly disappears. The police suspect Nick. Amy's friends reveal that she was afraid of him, that she kept secrets from him. He swears it isn't true. A police examination of his computer shows strange searches. He says they weren't made by him. And then there are the persistent calls on his mobile phone. So what did happen to Nick's beautiful wife?" },
                    { 20, 9, "The Da Vinci Code", new DateTime(2003, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "While in Paris, Harvard symbologist Robert Langdon is awakened by a phone call in the dead of the night. The elderly curator of the Louvre has been murdered inside the museum, his body covered in baffling symbols. As Langdon and gifted French cryptologist Sophie Neveu sort through the bizarre riddles, they are stunned to discover a trail of clues hidden in the works of Leonardo da Vinci—clues visible for all to see and yet ingeniously disguised by the painter. Even more startling, the late curator was involved in the Priory of Sion—a secret society whose members included Sir Isaac Newton, Victor Hugo, and Da Vinci—and he guarded a breathtaking historical secret. Unless Langdon and Neveu can decipher the labyrinthine puzzle—while avoiding the faceless adversary who shadows their every move—the explosive, ancient truth will be lost forever." },
                    { 21, 10, "A Good Girl's Guide to Murder", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The case is closed. Five years ago, schoolgirl Andie Bell was murdered by Sal Singh. The police know he did it. Everyone in town knows he did it. But having grown up in the same small town that was consumed by the murder, Pippa Fitz-Amobi isn't so sure. When she chooses the case as the topic for her final year project, she starts to uncover secrets that someone in town desperately wants to stay hidden. And if the real killer is still out there, how far will they go to keep Pip from the truth?" },
                    { 22, 11, "In a Sunburned Country", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "It is the driest, flattest, hottest, most infertile and climatically aggressive of all the inhabited continents, and still Australia teems with life - a large portion of it quite deadly. In fact, Australia has more things that can kill you in a very nasty way than anywhere else. Ignoring such dangers - and yet curiously obsessed by them - Bill Bryson journeyed to Australia and promptly fell in love with the country. And who can blame him? The people are cheerful, extroverted, quick-witted and unfailingly obliging: their cities are safe and clean and nearly always built on water; the food is excellent; the beer is cold and the sun nearly always shines. Life doesn't get much better than this..." },
                    { 23, 12, "Into the Wild", new DateTime(1996, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "In April, 1992, a young man from a well-to-do family hitchhiked to Alaska and walked alone into the wilderness north of Mt. McKinley. His name was Christopher Johnson McCandless. He had given $25,000 in savings to charity, abandoned his car and most of his possessions, burned all the cash in his wallet, and invented a new life for himself. Four months later, a party of moose hunters found his decomposed body. How McCandless came to die is the unforgettable story of Into the Wild. Immediately after graduating from college in 1991, McCandless had roamed through the West and Southwest on a vision quest like those made by his heroes Jack London and John Muir. In the Mojave Desert he abandoned his car, stripped it of its license plates, and burned all of his cash. He would give himself a new name, Alexander Supertramp, and, unencumbered by money and belongings, he would be free to wallow in the raw, unfiltered experiences that nature presented. Craving a blank spot on the map, McCandless simply threw away the maps. Leaving behind his desperate parents and sister, he vanished into the wild." },
                    { 24, 13, "The Shining", new DateTime(1977, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jack Torrance's new job at the Overlook Hotel is the perfect chance for a fresh start. As the off-season caretaker at the atmospheric old hotel, he'll have plenty of time to spend reconnecting with his family and working on his writing. But as the harsh winter weather sets in, the idyllic location feels ever more remote...and more sinister. And the only one to notice the strange and terrible forces gathering around the Overlook is Danny Torrance, a uniquely gifted five-year-old." },
                    { 25, 13, "It", new DateTime(1986, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Welcome to Derry, Maine ... It’s a small city, a place as hauntingly familiar as your own hometown. Only in Derry the haunting is real ... They were seven teenagers when they first stumbled upon the horror. Now they are grown-up men and women who have gone out into the big world to gain success and happiness. But none of them can withstand the force that has drawn them back to Derry to face the nightmare without an end, and the evil without a name." },
                    { 26, 13, "Carrie", new DateTime(1974, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A modern classic, Carrie introduced a distinctive new voice in American fiction -- Stephen King. The story of misunderstood high school girl Carrie White, her extraordinary telekinetic powers, and her violent rampage of revenge, remains one of the most barrier-breaking and shocking novels of all time. Make a date with terror and live the nightmare that is...Carrie" },
                    { 27, 13, "The Green Mile", new DateTime(1996, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "At Cold Mountain Penitentiary, along the lonely stretch of cells known as the Green Mile, condemned killers such as 'Billy the Kid' Wharton and the possessed Eduard Delacroix await death strapped in 'Old Sparky'. But good or evil, innocent or guilty, prisoner or guard, none has ever seen the brutal likes of the new prisoner, John Coffey, sentenced to death for raping and murdering two young girls. Is Coffey a devil in human form? Or is he a far, far different kind of being? There are more wonders in heaven and hell than anyone at Cold Mountain can imagine and one of those wonders might just have stepped in amongst them." },
                    { 28, 14, "The Haunting of Hill House", new DateTime(1959, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "It is the story of four seekers who arrive at a notoriously unfriendly pile called Hill House: Dr. Montague, an occult scholar looking for solid evidence of a \"haunting\"; Theodora, the lighthearted assistant; Eleanor, a friendless, fragile young woman well acquainted with poltergeists; and Luke, the future heir of Hill House. At first, their stay seems destined to be merely a spooky encounter with inexplicable phenomena. But Hill House is gathering its powers—and soon it will choose one of them to make its own." },
                    { 29, 15, "Dracula", new DateTime(1897, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "When Jonathan Harker visits Transylvania to help Count Dracula with the purchase of a London house, he makes a series of horrific discoveries about his client. Soon afterwards, various bizarre incidents unfold in England: an apparently unmanned ship is wrecked off the coast of Whitby; a young woman discovers strange puncture marks on her neck; and the inmate of a lunatic asylum raves about the 'Master' and his imminent arrival." },
                    { 30, 16, "Why We Sleep: Unlocking the Power of Sleep and Dreams", new DateTime(2017, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Neuroscientist and sleep expert Matthew Walker provides a revolutionary exploration of sleep, examining how it affects every aspect of our physical and mental well-being. Charting the most cutting-edge scientific breakthroughs, and marshalling his decades of research and clinical practice, Walker explains how we can harness sleep to improve learning, mood and energy levels, regulate hormones, prevent cancer, Alzheimer's and diabetes, slow the effects of aging, and increase longevity. He also provides actionable steps towards getting a better night's sleep every night." },
                    { 31, 17, "In Defense of Food: An Eater's Manifesto", new DateTime(2008, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Michael Pollan's last book, The Omnivore's Dilemma, launched a national conversation about the American way of eating; now In Defense of Food shows us how to change it, one meal at a time. Pollan proposes a new answer to the question of what we should eat that comes down to seven simple but liberating words: Eat food. Not too much. Mostly plants. Pollan's bracing and eloquent manifesto shows us how we can start making thoughtful food choices that will enrich our lives, enlarge our sense of what it means to be healthy, and bring pleasure back to eating." },
                    { 32, 18, "Joy of Cooking", new DateTime(1931, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "You will find tried-and-true favorites like Banana Bread Cockaigne, Chocolate Chip Cookies, and Southern Corn Bread—all retested and faithfully improved—as well as new favorites like Chana Masala, Beef Rendang, Megan’s Seeded Olive Oil Granola, and Smoked Pork Shoulder. In addition to a thoroughly modernized vegetable chapter, there are many more vegan and vegetarian recipes, including Caramelized Tamarind Tempeh, Crispy Pan-Fried Tofu, Spicy Chickpea Soup, and Roasted Mushroom Burgers. Joy’s baking chapters now include gram weights for accuracy, along with a refreshed lineup of baked goods like Cannelés de Bordeaux, Rustic No-Knead Sourdough, Ciabatta, Chocolate-Walnut Babka, and Chicago-Style Deep-Dish Pizza, as well as gluten-free recipes for pizza dough and yeast breads." },
                    { 33, 19, "Mastering the Art of French Cooking", new DateTime(1961, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "or over fifty years, New York Times bestseller Mastering the Art of French Cooking has been the definitive book on the subject for American readers. Featuring 524 delicious recipes, in its pages home cooks will find something for everyone, from seasoned experts to beginners who love good food and long to reproduce the savory delights of French cuisine, from historic Gallic masterpieces to the seemingly artless perfection of a dish of spring-green peas. Here Julia Child, Simone Beck, and Louisette Bertholle break down the classic foods of France into a logical sequence of themes and variations rather than presenting an endless and diffuse catalogue of dishes. Throughout, the focus is on key recipes that form the backbone of French cookery and lend themselves to an infinite number of elaborations--bound to increase anyone's culinary repertoire. With over 100 instructive illustrations to guide readers every step of the way, Mastering the Art of French Cooking deserves a place of honor in every kitchen in America." }
                });

            migrationBuilder.InsertData(
                table: "GenresInBook",
                columns: new[] { "GenresInBookId", "BookId", "GenreId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 1, 11 },
                    { 4, 1, 13 },
                    { 5, 2, 1 },
                    { 6, 2, 2 },
                    { 7, 2, 11 },
                    { 8, 2, 13 },
                    { 9, 3, 1 },
                    { 10, 3, 2 },
                    { 11, 3, 11 },
                    { 12, 3, 13 },
                    { 13, 4, 1 },
                    { 14, 4, 2 },
                    { 15, 4, 11 },
                    { 16, 4, 13 },
                    { 17, 5, 1 },
                    { 18, 5, 2 },
                    { 19, 5, 11 },
                    { 20, 5, 13 },
                    { 21, 6, 1 },
                    { 22, 6, 2 },
                    { 23, 6, 11 },
                    { 24, 6, 13 },
                    { 25, 7, 1 },
                    { 26, 7, 2 },
                    { 27, 7, 11 },
                    { 28, 7, 13 },
                    { 29, 8, 1 },
                    { 30, 8, 2 },
                    { 31, 8, 11 },
                    { 32, 8, 13 },
                    { 33, 9, 1 },
                    { 34, 9, 2 },
                    { 35, 9, 11 },
                    { 36, 9, 12 },
                    { 37, 10, 1 },
                    { 38, 10, 2 },
                    { 39, 10, 11 },
                    { 40, 10, 12 },
                    { 41, 11, 1 },
                    { 42, 11, 2 },
                    { 43, 11, 11 },
                    { 44, 11, 12 },
                    { 45, 12, 1 },
                    { 46, 12, 2 },
                    { 47, 12, 11 },
                    { 48, 12, 12 },
                    { 49, 13, 11 },
                    { 50, 13, 3 },
                    { 51, 13, 6 },
                    { 52, 13, 4 },
                    { 53, 14, 3 },
                    { 54, 14, 11 },
                    { 55, 14, 12 },
                    { 56, 15, 3 },
                    { 57, 15, 11 },
                    { 58, 15, 12 },
                    { 59, 16, 3 },
                    { 60, 16, 11 },
                    { 61, 16, 12 },
                    { 62, 17, 3 },
                    { 63, 17, 11 },
                    { 64, 17, 12 },
                    { 65, 18, 15 },
                    { 66, 18, 16 },
                    { 67, 18, 4 },
                    { 68, 18, 11 },
                    { 69, 19, 11 },
                    { 70, 19, 4 },
                    { 71, 19, 16 },
                    { 72, 19, 15 },
                    { 73, 19, 14 },
                    { 74, 20, 11 },
                    { 75, 20, 4 },
                    { 76, 20, 16 },
                    { 77, 20, 15 },
                    { 78, 20, 14 },
                    { 79, 21, 11 },
                    { 80, 21, 4 },
                    { 81, 21, 16 },
                    { 82, 21, 15 },
                    { 83, 21, 12 },
                    { 84, 22, 17 },
                    { 85, 22, 8 },
                    { 86, 22, 10 },
                    { 87, 23, 10 },
                    { 88, 23, 11 },
                    { 89, 23, 2 },
                    { 90, 24, 5 },
                    { 91, 24, 4 },
                    { 92, 24, 1 },
                    { 93, 24, 14 },
                    { 94, 24, 11 },
                    { 95, 25, 5 },
                    { 96, 25, 4 },
                    { 97, 25, 1 },
                    { 98, 25, 16 },
                    { 99, 25, 14 },
                    { 100, 25, 11 },
                    { 101, 26, 11 },
                    { 102, 26, 5 },
                    { 103, 26, 1 },
                    { 104, 26, 16 },
                    { 105, 26, 14 },
                    { 106, 27, 5 },
                    { 107, 27, 11 },
                    { 108, 27, 1 },
                    { 109, 27, 16 },
                    { 110, 27, 4 },
                    { 111, 27, 15 },
                    { 112, 27, 6 },
                    { 113, 28, 5 },
                    { 114, 28, 11 },
                    { 115, 28, 4 },
                    { 116, 28, 14 },
                    { 117, 28, 16 },
                    { 118, 29, 5 },
                    { 119, 29, 11 },
                    { 120, 29, 1 },
                    { 121, 30, 17 },
                    { 122, 30, 9 },
                    { 123, 31, 17 },
                    { 124, 31, 9 },
                    { 125, 31, 7 },
                    { 126, 32, 7 },
                    { 127, 32, 17 },
                    { 128, 33, 7 },
                    { 129, 33, 17 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_GenresInBook_BookId",
                table: "GenresInBook",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_GenresInBook_GenreId",
                table: "GenresInBook",
                column: "GenreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenresInBook");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
