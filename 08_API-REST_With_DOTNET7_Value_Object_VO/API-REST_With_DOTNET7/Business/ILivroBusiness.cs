﻿using API_REST_With_DOTNET7.Data.VO;
using API_REST_With_DOTNET7.Model;

namespace API_REST_With_DOTNET7.Business
{
    public interface ILivroBusiness
    {
        //List<Livro> FindAllBusiness();
        //Livro FindByIdBusiness(int id);
        //Livro CreateBusiness(Livro livro);
        //Livro UpdateBusiness(Livro livro);
        //void DeleteBusiness(int id);

        // VO
        List<LivroVO> FindAllBusiness();
        LivroVO FindByIdBusiness(int id);
        LivroVO CreateBusiness(LivroVO livro);
        LivroVO UpdateBusiness(LivroVO livro);
        void DeleteBusiness(int id);
    }
}
