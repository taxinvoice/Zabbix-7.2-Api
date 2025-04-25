﻿using Zabbix.Core;
using Zabbix.Entities;
using Zabbix.Helpers;

namespace Zabbix.Services.CrudServices;

public interface IUpdateService<TEntity, TResult>
    where TEntity : BaseEntity
{
    IEnumerable<TResult> Update(IEnumerable<TEntity> entities);
    TResult Update(TEntity entity);
    Task<IEnumerable<TResult>> UpdateAsync(IEnumerable<TEntity> entities);
    Task<TResult> UpdateAsync(TEntity entity);
}

public class UpdateService<TEntity, TEntityResult> : ServiceBase, IUpdateService<TEntity, string>
    where TEntity : BaseEntity
    where TEntityResult : BaseResult
{
    public UpdateService(ICore core, string className)
        : base(core, className)
    {
    }

    public virtual IEnumerable<string> Update(IEnumerable<TEntity> entities)
    {
        var ret = Core.SendRequest<TEntityResult>(entities, ClassName + ".update").Ids;
        return Checker.ReturnEmptyListOrActual(ret);
    }

    public virtual string Update(TEntity entity)
    {
        string? ret = Update(new List<TEntity> { entity }).FirstOrDefault();
        return Checker.ReturnEmptyStringOrActual(ret);
    }

    public virtual async Task<IEnumerable<string>> UpdateAsync(IEnumerable<TEntity> entities)
    {
        var ret = (await Core.SendRequestAsync<TEntityResult>(entities, ClassName + ".update")).Ids;
        return Checker.ReturnEmptyListOrActual(ret);
    }

    public virtual async Task<string> UpdateAsync(TEntity entity)
    {
        string? ret = (await UpdateAsync(new List<TEntity> { entity })).FirstOrDefault();
        return Checker.ReturnEmptyStringOrActual(ret);
    }
}
