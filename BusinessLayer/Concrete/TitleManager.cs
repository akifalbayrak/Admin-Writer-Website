using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstracs;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class TitleManager : ITitleService
    {
        ITitleDal _titleDal;

        public TitleManager(ITitleDal titleDal)
        {
            _titleDal = titleDal;
        }

        public void TitleAdd(Title title)
        {
            _titleDal.Insert(title);
        }

        public Title GetById(int id)
        {
            return _titleDal.Get(x => x.TitleId == id);
        }

        public List<Title> GetList()
        {
            return _titleDal.List();
        }

        public void TitleDelete(Title title)
        {
            _titleDal.Update(title);
        }

        public void TitleUpdate(Title title)
        {
            _titleDal.Update(title);
        }

        public List<Title> GetListByWriter(int id)
        {
            return _titleDal.List(x=>x.WriterId == id);
        }
    }
}
