using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MainLibrary.Clients
{
    public class ClientsConverter : JsonConverter<Client>
    {
        /// <summary>
        /// Тип клиента (физ/юр лицо)
        /// </summary>
        private enum TypeDiscriminator
        {
            Person = 0,
            Entity = 1
        }

        public override bool CanConvert(Type type)
        {
            return typeof(Client).IsAssignableFrom(type);
        }

        public override Client Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            if (!reader.Read()
                    || reader.TokenType != JsonTokenType.PropertyName
                    || reader.GetString() != "TypeDiscriminator")
            {
                throw new JsonException();
            }

            if (!reader.Read() || reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            Client client;
            TypeDiscriminator typeDiscriminator = (TypeDiscriminator)reader.GetInt32();
            switch (typeDiscriminator)
            {
                case TypeDiscriminator.Entity:
                    if (!reader.Read() || reader.GetString() != "TypeValue")
                    {
                        throw new JsonException();
                    }
                    if (!reader.Read() || reader.TokenType != JsonTokenType.StartObject)
                    {
                        throw new JsonException();
                    }
                    client = (Entity)JsonSerializer.Deserialize(ref reader, typeof(Entity));
                    break;

                case TypeDiscriminator.Person:
                    if (!reader.Read() || reader.GetString() != "TypeValue")
                    {
                        throw new JsonException();
                    }
                    if (!reader.Read() || reader.TokenType != JsonTokenType.StartObject)
                    {
                        throw new JsonException();
                    }
                    client = (Person)JsonSerializer.Deserialize(ref reader, typeof(Person));
                    break;

                default:
                    throw new NotSupportedException();
            }

            if (!reader.Read() || reader.TokenType != JsonTokenType.EndObject)
            {
                throw new JsonException();
            }

            return client;
        }

        public override void Write(Utf8JsonWriter writer, Client value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            if (value is Entity worker)
            {
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.Entity);
                writer.WritePropertyName("TypeValue");
                JsonSerializer.Serialize(writer, worker);
            }
            else if (value is Person intern)
            {
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.Person);
                writer.WritePropertyName("TypeValue");
                JsonSerializer.Serialize(writer, intern);
            }
            else
            {
                throw new NotSupportedException();
            }

            writer.WriteEndObject();
        }
    }
}