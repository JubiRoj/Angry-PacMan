﻿using System;
using Microsoft.Xna.Framework;

namespace pacmangame
{
    // класс "враг"
    public class Enemy
    {
        public Vector2 Screenpos; // позиция на экране
        public Vector2 Center; // ось вращения
        public Vector2 Velocity; // векторы скорости
        public bool IsAlive; // была ли цель уничтожена
        public float Rotation; //поворот
        public float Speed;//скорость
        private Random Rnd; // счетчик случайных чисел
        private int IncreaseSpeedCount; // промежуточное поле для хранения счетчика увеличения скорости

        // конструктор класса - действия, которые осуществляются при его создании (инициализации)
        public Enemy(int x,int y)
        {
            Speed = 4;
            Center.X = 40;
            Center.Y = 40;
            IsAlive = true;
            Screenpos.X = x; 
            Screenpos.Y = y;
            Velocity = new Vector2(20, 20);
        }

        //  метод обработки целей
        public void Process(bool enableSpeedUp, GameTime gameTime) 
        {
            Rnd = new Random(); // инициализация счетчика случайных чисел
            if (enableSpeedUp) // если допустимо ускорение 
            {
                //увеличение скорости до определенного предела
                IncreaseSpeedCount++;
				if (IncreaseSpeedCount >= 400) // спустя некоторое время скорость должна начать увеличиваться
                {
                    if (Speed<30) Speed += 0.001f;
                }
            }
            else Speed = 2; // в противном случае - скорость постоянна
            if (!IsAlive) // если цель была уничтожена - новое появление на случайных позициях экрана
            {
                    IsAlive = true; // цель не была уничтожена
                    switch (Rnd.Next(1, 5)) // выбор координат в зависимости от ситуации
                    {
                        case 1:
                            Screenpos.X = Rnd.Next(40,100);
                            Screenpos.Y = 40;
                            break;
                        case 2:
                            Screenpos.X = 550;
                            Screenpos.Y = Rnd.Next(40, 100);
                            break;
                        case 3:
                            Screenpos.X = 40;
                            Screenpos.Y = Rnd.Next(400,500);
                            break;
                        case 4:
                            Screenpos.X = Rnd.Next(500, 600);
                            Screenpos.Y = 450;
                            break;
                }
            }
            Screenpos += Speed * Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds; // движение по экрану
            Rotation += ((Rnd.Next(1, 2))) * (float)gameTime.ElapsedGameTime.TotalSeconds; // изменение угла
            // отталкивание от края экрана
            if (Screenpos.X < 0 + Center.X) Velocity.X = -Velocity.X;
            if (Screenpos.X > 960 - Center.X) Velocity.X = -Velocity.X;
            if (Screenpos.Y < 0 + Center.X) Velocity.Y = -Velocity.Y;
            if (Screenpos.Y > 540 - Center.X) Velocity.Y = -Velocity.Y;
        }
    }
}
