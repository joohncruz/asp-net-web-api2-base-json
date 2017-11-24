using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace WebApp.Models
{
    public class Aluno
    {
        public int? id { get; set; }
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string telefone { get; set; }
        public int ra { get; set; }

        public List<Aluno> ListarAlunos()
        {

            var file = HostingEnvironment.MapPath(@"~/App_Data\Base.json");

            var json = File.ReadAllText(file);

            var listaAlunos = JsonConvert.DeserializeObject<List<Aluno>>(json);

            return listaAlunos;
        }

        public bool RescreverArquivo(List<Aluno> listaAlunos)
        {
            var file = HostingEnvironment.MapPath(@"~/App_Data\Base.json");
            var json = JsonConvert.SerializeObject(listaAlunos, Formatting.Indented);
            File.WriteAllText(file, json);

            return true;
        }

        public Aluno Inserir(Aluno aluno)
        {
            var listaAlunos = ListarAlunos();

            var maxId = listaAlunos.Max(x => x.id);
            aluno.id = maxId + 1;
            listaAlunos.Add(aluno);

            RescreverArquivo(listaAlunos);

            return aluno;
        }

        public Aluno Atualizar(int id, Aluno aluno)
        {
            var listaAlunos = ListarAlunos();

            var itemIdenx = listaAlunos.FindIndex(p => p.id == aluno.id);
            if (itemIdenx >= 0)
            {
                aluno.id = id;
                listaAlunos[itemIdenx] = aluno;
            } else
            {
                return null;
            }

            RescreverArquivo(listaAlunos);
            return aluno;

        }

        public bool Deletar(int id)
        {
            var listaAlunos = ListarAlunos();

            var itemIndex = listaAlunos.FindIndex(p => p.id == id);
            if (itemIndex >= 0)
            {
                listaAlunos.RemoveAt(itemIndex);
            } else
            {
                return false;
            }

            RescreverArquivo(listaAlunos);
            return true;
        }
    }
}