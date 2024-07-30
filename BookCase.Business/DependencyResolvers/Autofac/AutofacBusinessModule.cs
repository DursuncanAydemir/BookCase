using Autofac;
using Autofac.Extras.DynamicProxy;
using BookCase.Business.Abstract;
using BookCase.Business.Concrete;
using BookCase.Core.Helpers.FileHelper;
using BookCase.Core.Interceptors;
using BookCase.Core.Utilities.Security.JWT;
using BookCase.DataAccess.Abstract;
using BookCase.DataAccess.Concrete.EntityFramework;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookCase.Business.DependencyResolvers.Autofac;

public class AutofacBusinessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UserManager>().As<IUserService>();
        builder.RegisterType<EfUserDal>().As<IUserDal>();

        builder.RegisterType<NoteManager>().As<INoteService>();
        builder.RegisterType<EfNoteDal>().As<INoteDal>();

        builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();
        builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance();

        builder.RegisterType<FileHelperManager>().As<IFileHelper>().SingleInstance();

        builder.RegisterType<BookManager>().As<IBookService>();
        builder.RegisterType<EfBookDal>().As<IBookDal>();

        var assembly = System.Reflection.Assembly.GetExecutingAssembly();

        builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
            .EnableInterfaceInterceptors(new ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();
    }
}
