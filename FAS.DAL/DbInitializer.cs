using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using FAS.DAL.Identity;
using FAS.Domain;

namespace FAS.DAL
{
    internal class DbInitializer : CreateDatabaseIfNotExists<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            //var user = new[]
            //{
            //    new User("admin", "admin", "admin", "admin", "admin", "admin", 9999),
            //    new User("Vinikite@mail.ru", "!QAZ2wsx", "Никита", "Шилов", "Даниилович", "Vinikite@mail.ru", 456),
            //    new User("Qwerty@mail.ru","!QAZ2wsx","Александр","Лукашенко","Григорьевич","Qwerty@mail.ru",456),
            //};
            //context.Users.Add(user);
            //context.SaveChanges();

            //Address b1 = new Address("Польша", "Варшава", "Belwederska", "3", "5");
            //b1.Users.Add(user[0]);
            //Address b2 = new Address("Австрия", "Вена", "Augustinerstraße", "32", "15");
            //b2.Users.Add(user[1]);
            //Address b3 = new Address("Германия", "Роммерскирхен", "Domweg ", "4", "1");
            //b3.Users.Add(user[2]);
            //context.Addresses.AddRange(new[] { b1, b2, b3 });
            //context.SaveChanges();

            ////
            var statusScore = new[]
            {
                new StatusScore("В обработке"),
                new StatusScore("Выполнен"),
                new StatusScore("Не выполнено"),
                new StatusScore("Ошибка")
            };
            context.StatusScores.AddRange(statusScore);
            context.SaveChanges();

            var typeScore = new[]
             {
                new TypeScore("Текущий счет"),
                new TypeScore("Депозитный счет"),
                new TypeScore("Карточный счет"),
            };
            context.TypeScores.AddRange(typeScore);
            context.SaveChanges();

            var viewScore = new[]
            {
                new ViewScore("Расчетный счет"),
                new ViewScore("Валютный счет"),
                new ViewScore("Лицевой счет"),
            };
            context.ViewScores.AddRange(viewScore);
            context.SaveChanges();

            var category = new[]
            {
                new Category("Автомобиль"),
                new Category("Дети"),
                new Category("Аренда жилья"),
                new Category("Домашнее хозяйство"),
                new Category("Домашние животные"),
                new Category("Досуг и отдых"),
                new Category("Инвистичионный доход"),
                 new Category("Налоги, сборы и услуги"),
                  new Category("Инвестиционный расход"),
                   new Category("Коммунальные платежи"),
                    new Category("Медицина"),
                     new Category("Мотоцикл"),
                      new Category(""),
                       new Category("Не определена. Для доходов"),
                        new Category("Не определена. Для расходов"),
                         new Category("Образование"),
                new Category("дежда, обувь, аксессуары"),
                new Category("Персональные доходы"),
                new Category("Питание"),
                new Category("Подарки, материальная помощь"),
                new Category("Проезд, транспорт"),
                new Category("Прочие доходы"),
                new Category("Прочие личные расходы"),
                new Category("Связь, ТВ и интернет"),
                new Category("Уход за собой"),
            };
            context.Categories.AddRange(category);
            context.SaveChanges();

            var transactionType = new[]
            {
                new TransactionType("Поступление средств"),
                new TransactionType("Снятие со счета"),
                new TransactionType("Выписка"),
            };
            context.TransactionTypes.AddRange(transactionType);
            context.SaveChanges();

            var bank = new[]
{
                new Bank("Идея Банк"),
                new Bank("Банк БелВЭБ"),
                new Bank("Банк Решение"),
                 new Bank(""),
                new Bank("Абсолютбанк"),
                new Bank("Альфа-Банк"),
                 new Bank(""),
                new Bank("БПС-Сбербанк"),
                new Bank("БСБ Банк (БелСвиссБанк)"),
                new Bank("БТА Банк"),
                 new Bank("ВТБ Беларусь"),
                new Bank("БелГазпромБанк"),
                new Bank("БелАгроПромБанк"),
                new Bank("БеларусБанк"),
                new Bank("БелИнвестБанк"),
            };
            context.Banks.AddRange(bank);
            context.SaveChanges();

            AppRoleManager roleManager = new AppRoleManager(new RoleStore<Role, Guid, UserRole>(context));
            roleManager.CreateAsync(new Role() { Name = "User" }).Wait();
            roleManager.CreateAsync(new Role() { Name = "Admin" }).Wait();

        }
    }
}
