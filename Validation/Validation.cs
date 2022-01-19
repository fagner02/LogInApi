using System.Text.RegularExpressions;
using System;
namespace LogInApi.Validations {
    public static class Validation {
        public static bool ValidateAge(DateTime birthDate) {
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age)) age--;
            return age >= 18;
        }

        public static bool ValidateCpf(string cpf) {
            cpf = Regex.Replace(cpf, @"[^0-9]", "");

            if (cpf.Length != 11) {
                return false;
            }

            int res1 = 0;
            for (int i = 10; i >= 2; i--) {
                res1 += int.Parse(cpf[10 - i].ToString()) * i;
            }

            res1 = (res1 * 10 % 11 == 10) ? 0 : res1 * 10 % 11;

            if (res1 != int.Parse(cpf[9].ToString())) {
                return false;
            }

            int res2 = 0;
            for (int i = 11; i >= 2; i--) {
                res2 += int.Parse(cpf[11 - i].ToString()) * i;
            }

            res2 = (res2 * 10 % 11 == 10) ? 0 : res2 * 10 % 11;

            if (res2 != int.Parse(cpf[10].ToString())) {
                return false;
            }

            return true;
        }
    }
}