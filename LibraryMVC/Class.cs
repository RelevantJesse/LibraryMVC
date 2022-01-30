namespace LibraryMVC.UI
{
    public class Program
    {
        public void Mainasdf()
        {

            Console.WriteLine("What kind of animal do you want?");
            var input = Console.ReadLine();
            Animal animal;
            if (input == "dog")
            {
                animal = new Dog();
            }
            else
            {
                animal = new Cat();
            }

            animal.Breathe();
        }
        
    }

    public class Dog : Animal
    {
        public string Name { get; set; }

        public override void Breathe()
        {
            Console.WriteLine("I'm a dog breathing");
        }

        public override void Move()
        {
            Console.WriteLine("I'm a dog moving");
        }

        public void Bark()
        {
            Console.WriteLine("Woof!");
        }
    }

    public class Cat : Animal
    {
        public string Name { get; set; }


        public override void Move()
        {
            Console.WriteLine("I'm a cat moving");
        }
    }

    public abstract class Animal
    {
        public string Name { get; set; }

        public virtual void Breathe()
        {
            Console.WriteLine("I'm breathing");
        }
        public abstract void Move();
    }
}
