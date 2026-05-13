using Noise.Models;

namespace Noise.Data;

public static class DbInitializer
{
    public static void Seed(ApplicationDbContext context)
    {
        if (context.Countries.Any())
        {
            return;
        }

        var usa = new Country
        {
            Name = "США"
        };

        var uk = new Country
        {
            Name = "Велика Британія"
        };

        var jamaica = new Country
        {
            Name = "Ямайка"
        };

        context.Countries.AddRange(usa, uk, jamaica);
        context.SaveChanges();

        var rock = new Genre
        {
            Name = "Рок",
            Description = "Музичний жанр, що виник у середині XX століття та став одним із найвпливовіших напрямів сучасної музики.",
            YearOfOrigin = 1950,
            CountryId = usa.Id
        };

        var hipHop = new Genre
        {
            Name = "Хіп-хоп",
            Description = "Культурний і музичний напрям, що виник у США в 1970-х роках та поєднує реп, ритм, соціальні тексти й вуличну культуру.",
            YearOfOrigin = 1970,
            CountryId = usa.Id
        };

        var reggae = new Genre
        {
            Name = "Реггі",
            Description = "Музичний жанр, що виник на Ямайці та має характерний ритм, пов’язаний із соціальними й духовними темами.",
            YearOfOrigin = 1960,
            CountryId = jamaica.Id
        };

        context.Genres.AddRange(rock, hipHop, reggae);
        context.SaveChanges();

        var beatles = new Artist
        {
            Name = "The Beatles",
            StartYear = 1960,
            EndYear = 1970,
            Biography = "Британський гурт, який суттєво вплинув на розвиток рок-музики та популярної культури.",
            CountryId = uk.Id,
            GenreId = rock.Id
        };

        var bobMarley = new Artist
        {
            Name = "Bob Marley",
            StartYear = 1962,
            EndYear = 1981,
            Biography = "Ямайський виконавець, який популяризував реггі у світі.",
            CountryId = jamaica.Id,
            GenreId = reggae.Id
        };

        var tupac = new Artist
        {
            Name = "2Pac",
            StartYear = 1987,
            EndYear = 1996,
            Biography = "Американський репер, поет і актор, один із найвідоміших представників хіп-хоп культури.",
            CountryId = usa.Id,
            GenreId = hipHop.Id
        };

        context.Artists.AddRange(beatles, bobMarley, tupac);
        context.SaveChanges();

        var abbeyRoad = new MusicItem
        {
            Title = "Abbey Road",
            ReleaseYear = 1969,
            Type = "Альбом",
            Description = "Один із найвідоміших альбомів The Beatles, який вважається важливим етапом в історії рок-музики.",
            ArtistId = beatles.Id,
            GenreId = rock.Id
        };

        var exodus = new MusicItem
        {
            Title = "Exodus",
            ReleaseYear = 1977,
            Type = "Альбом",
            Description = "Альбом Bob Marley & The Wailers, що став одним із символів світового реггі.",
            ArtistId = bobMarley.Id,
            GenreId = reggae.Id
        };

        var allEyezOnMe = new MusicItem
        {
            Title = "All Eyez on Me",
            ReleaseYear = 1996,
            Type = "Альбом",
            Description = "Один із найвідоміших альбомів 2Pac, важливий для історії хіп-хопу.",
            ArtistId = tupac.Id,
            GenreId = hipHop.Id
        };

        context.MusicItems.AddRange(abbeyRoad, exodus, allEyezOnMe);
        context.SaveChanges();

        context.MediaItems.AddRange(
            new MediaItem
            {
                Title = "Abbey Road Cover",
                Type = "image",
                UrlOrPath = "/images/abbey-road.jpg",
                Description = "Обкладинка альбому Abbey Road.",
                MusicItemId = abbeyRoad.Id
            },
            new MediaItem
            {
                Title = "Exodus Cover",
                Type = "image",
                UrlOrPath = "/images/exodus.jpg",
                Description = "Обкладинка альбому Exodus.",
                MusicItemId = exodus.Id
            },
            new MediaItem
            {
                Title = "All Eyez on Me Cover",
                Type = "image",
                UrlOrPath = "/images/all-eyez-on-me.jpg",
                Description = "Обкладинка альбому All Eyez on Me.",
                MusicItemId = allEyezOnMe.Id
            }
        );

        context.News.AddRange(
            new News
            {
                Title = "Відкриття цифрової експозиції Noise",
                Text = "У вебсистемі Noise відкрито першу цифрову експозицію, присвячену розвитку сучасної музики.",
                PublishedAt = DateTime.Now,
                ImageUrl = "/images/news-1.jpg"
            },
            new News
            {
                Title = "Оновлення розділу музичних жанрів",
                Text = "До музею додано нові матеріали про рок, хіп-хоп та реггі.",
                PublishedAt = DateTime.Now,
                ImageUrl = "/images/news-2.jpg"
            }
        );

        context.SaveChanges();
    }
}