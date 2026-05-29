namespace ProductAPI.Application.Interface.Mapper
{
    public interface IBaseMapper<TEntity, TEntityDto>
        where TEntity : class
        where TEntityDto : class
    {
        TEntityDto MapperEntityToDto(TEntity entity);
        IEnumerable<TEntityDto> MapperListEntityToDto(IEnumerable<TEntity> entities);
        TEntity MapperDtoToEntity(TEntityDto dto);
    }
}
