using System.Threading.Tasks;
using dio_primeira_api_dotnet.Controllers;
using dio_primeira_api_dotnet.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace dio_api_dotnet_tests_xunit
{
    public class CategoriasControllerTest
    {
        
        private readonly Mock<DbSet<Categoria>> _mockSet;
        private readonly Mock<Context> _mockContext;
        private readonly Categoria _categoria;
        
        public CategoriasControllerTest()
        {
            _mockSet = new Mock<DbSet<Categoria>>();
            _mockContext = new Mock<Context>();
            _categoria = new Categoria { id = 1, Descricao = "Teste Categoria" };
            
            _mockContext.Setup(m => m.Categorias).Returns(_mockSet.Object);
            _mockContext.Setup(m => m.Categorias.FindAsync(1)).ReturnsAsync(_categoria);
            
        }

        [Fact]
        public async Task Get_Categoria()
        {
            var service = new CategoriasController(_mockContext.Object);

            var testCategoria = await service.GetCategoria(1);
            
            //verifica se o FindAsync foi executado apenas 1x
            _mockSet.Verify(m => m.FindAsync(1), Times.Once());
            
            Assert.Equal(_categoria.id, testCategoria.Value.id);
        }
    }
}