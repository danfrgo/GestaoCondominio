using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GestaoCondominios.DAL.Interfaces
{
    public interface IRepositorioGenerico<TEntity> where TEntity: class
    {
        Task<IEnumerable<TEntity>> ObterTodos();
        Task<TEntity> ObterPeloId(int id);
        Task<TEntity> ObterPeloId(string id);
        Task Inserir(TEntity entity);

        Task Inserir(List<TEntity> entity);
        Task Atualizar(TEntity entity);
        Task Excluir(TEntity entity);
        Task Excluir(int id);
        Task Excluir(string id);
    }
}
