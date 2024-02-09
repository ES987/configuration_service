using ConfigurationService.Database.Providers.Interfaces;
using ConfigurationService.Entities.Repositories.Interfaces;

namespace ConfigurationService.Entities.Repositories.Repositories
{
    public class DBCreater
    {
        public async Task Create(IDbProvider provider, string dataBase)
        {
             

            List<ITableCreater> creaters = new List<ITableCreater>();
            creaters.Add(new ProgramsRepository.TableCreater(provider));
            creaters.Add(new ProvidersRepository.TableCreater(provider));

            foreach (var item in creaters)
            {
                try
                {
                    await item.Create();
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
