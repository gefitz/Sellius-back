namespace Sellius.API.Repository.Interfaces
{
    public interface IDbMethods<model>
    {
        public Task<bool> Create(model obj);
        public Task<bool> Update(model obj);
        public Task<bool> Delete(model obj);
        public Task<IEnumerable<model>> Filtrar(model obj);
        public Task<model> BuscaDireto(model idObjeto);
        
    }
}
