#include <iostream>
#include <vector>

using namespace std;

// Абстрактные базовые классы всех возможных видов воинов

//Пехитинец
class Infantryman
{
  public:
    virtual void info() = 0;
    virtual ~Infantryman() {}
};

//Стрелец
class Archer
{
  public:
    virtual void info() = 0;
    virtual ~Archer() {}
};

//Всадник
class Horseman
{
  public:
    virtual void info() = 0;
    virtual ~Horseman() {}
};

class RussianInfantryman : public Infantryman {
    public:
        void info() {
            cout << "RUSSIAN Infantryman"<<endl;
        }
};

class RussianArcher : public Archer {
    public:
        void info () {

            cout << "RUSSIAN Archer"<<endl;
        }
};

class RussianHorseman : public Horseman {
    public:
        void info () {
            cout << "RISSIAN Horseman"<<endl;
        }
};


// Классы всех видов воинов Римской армии
class RomanInfantryman: public Infantryman
{
  public:
    void info() {
      cout << "RomanInfantryman" << endl;
    }
};

class RomanArcher: public Archer
{
  public:
    void info() {
      cout << "RomanArcher" << endl;
    }
};

class RomanHorseman: public Horseman
{
  public:
    void info() {
      cout << "RomanHorseman" << endl;
    }
};



// Классы всех видов воинов армии Карфагена
class CarthaginianInfantryman: public Infantryman
{
  public:
    void info() {
      cout << "CarthaginianInfantryman" << endl;
    }
};

class CarthaginianArcher: public Archer
{
  public:
    void info() {
      cout << "CarthaginianArcher" << endl;
    }
};

class CarthaginianHorseman: public Horseman
{
  public:
    void info() {
      cout << "CarthaginianHorseman" << endl;
    }
};


// Абстрактная фабрика для производства воинов
class ArmyFactory
{
  public:
    virtual Infantryman* createInfantryman() = 0;
    virtual Archer* createArcher() = 0;
    virtual Horseman* createHorseman() = 0;
    virtual ~ArmyFactory() {}
};


// Фабрика для создания воинов Римской армии
class RomanArmyFactory: public ArmyFactory
{
  public:
    Infantryman* createInfantryman() {
      return new RomanInfantryman;
    }
    Archer* createArcher() {
      return new RomanArcher;
    }
    Horseman* createHorseman() {
      return new RomanHorseman;
    }
};

class RussianArmyFactory: public ArmyFactory {
    public:
        Infantryman* createInfantryman () {
            return new RussianInfantryman;
        }
        Archer* createArcher () {
            return new RussianArcher;
        }
        Horseman* createHorseman () {
            return new RussianHorseman;
        }
};


// Фабрика для создания воинов армии Карфагена
class CarthaginianArmyFactory: public ArmyFactory
{
  public:
    Infantryman* createInfantryman() {
      return new CarthaginianInfantryman;
    }
    Archer* createArcher() {
      return new CarthaginianArcher;
    }
    Horseman* createHorseman() {
      return new CarthaginianHorseman;
    }
};


// Класс, содержащий всех воинов той или иной армии
class Army
{
  public:
   ~Army() {
      int i;
      for(i=0; i<vi.size(); ++i)  delete vi[i];
      for(i=0; i<va.size(); ++i)  delete va[i];
      for(i=0; i<vh.size(); ++i)  delete vh[i];
    }
    void info() {
      int i;
      for(i=0; i<vi.size(); ++i)  vi[i]->info();
      for(i=0; i<va.size(); ++i)  va[i]->info();
      for(i=0; i<vh.size(); ++i)  vh[i]->info();
    }
    vector<Infantryman*> vi;
    vector<Archer*> va;
    vector<Horseman*> vh;
};


// Здесь создается армия той или иной стороны
class Game
{
  public:
    Army* createArmy( ArmyFactory& factory ) {
      Army* p = new Army;
      p->vi.push_back( factory.createInfantryman());
      p->va.push_back( factory.createArcher());
      p->vh.push_back( factory.createHorseman());
      return p;
    }
};


int main()
{
    Game game;
    RomanArmyFactory ra_factory;
    CarthaginianArmyFactory ca_factory;
    RussianArmyFactory ru_factory;

    Army * ra = game.createArmy( ra_factory);
    Army * ca = game.createArmy( ca_factory);
    Army* rra=game.createArmy(ru_factory);
    cout << "Roman army:" << endl;
    ra->info();
    cout << "\nCarthaginian army:" << endl;
    ca->info();
    cout << "RUSSIAN ARMY:"<<endl;
    rra->info();
    // ...
}
