using BattleSimulation.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BattleSimulation.Controllers
{
    public class BattleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult StartFight(FighterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var fighter1 = model.Fighter1;
            var fighter2 = model.Fighter2;

            var battleData = new List<object>();
            var random = new Random();

            int round = 1;
            while (fighter1.Health > 0 && fighter2.Health > 0)
            {
                // Боец 1 атакует Бойца 2
                var damage1 = CalculateDamage(fighter1, fighter2, random);
                fighter2.Health -= damage1;

                battleData.Add(new
                {
                    Round = round,
                    Attacker = fighter1.Name,
                    Defender = fighter2.Name,
                    Damage = damage1,
                    DefenderHealth = Math.Max(fighter2.Health, 0)
                });

                if (fighter2.Health <= 0)
                {
                    battleData.Add(new
                    {
                        Round = round,
                        Message = $"{fighter2.Name} повержен!"
                    });
                    break;
                }

                // Боец 2 атакует Бойца 1
                var damage2 = CalculateDamage(fighter2, fighter1, random);
                fighter1.Health -= damage2;

                battleData.Add(new
                {
                    Round = round,
                    Attacker = fighter2.Name,
                    Defender = fighter1.Name,
                    Damage = damage2,
                    DefenderHealth = Math.Max(fighter1.Health, 0)
                });

                if (fighter1.Health <= 0)
                {
                    battleData.Add(new
                    {
                        Round = round,
                        Message = $"{fighter1.Name} повержен!"
                    });
                    break;
                }

                round++;
            }

            // Сохраняем данные боя в TempData
            TempData["BattleData"] = Newtonsoft.Json.JsonConvert.SerializeObject(battleData);

            return View("FightResult");
        }

        [HttpGet]
        public IActionResult StartFight()
        {
            // Логика получения данных боя из TempData
            if (TempData["BattleData"] != null)
            {
                var battleDataJson = TempData["BattleData"] as string;
                return Content(battleDataJson, "application/json");
            }
            else
            {
                return BadRequest();
            }
        }

        private int CalculateDamage(Fighter attacker, Fighter defender, Random random)
        {
            // Расчет вероятности попадания
            double hitChance = (double)attacker.Agility / (attacker.Agility + defender.Agility) * 100;
            double hitRoll = random.NextDouble() * 100;

            if (hitRoll > hitChance)
            {
                // Атака не попала
                return 0;
            }

            // Базовый урон
            int baseDamage = attacker.Strength - defender.Defense;

            // Модификатор массы (например, +10% урона за каждые 10 единиц массы сверх 50)
            double massModifier = 1 + ((double)(attacker.Mass - 50) / 500);

            // Случайный фактор урона (±10%)
            double randomFactor = 1 + (random.NextDouble() * 0.2 - 0.1);

            // Итоговый урон
            int damage = (int)Math.Max(baseDamage * massModifier * randomFactor, 0);

            return damage;
        }
    }
}
