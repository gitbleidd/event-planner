import React, { useState } from "react";
import ListGroup from 'react-bootstrap/ListGroup';
import { NavLink } from "react-router-dom";
import "./Events.css";
import * as routes from "../shared/routes";

function Events() {
  const [events, setEvents] = useState([
    {
      id: 0,
      type: {
        id: 1,
        name: "Концерт"
      },
      name: '«Музыкальный вечер в стиле джаз»',
      organizerName: "Джаз-клуб «Blue Note»",
      locationName: "Ул. Ленина 91, Концертный зал «Маяк»",
      cost: 1000,
      description: "Вас ждет увлекательный музыкальный вечер, полный живого звука и великолепного джаза. На сцене выступят известные джаз-музыканты, которые исполнят лучшие произведения жанра. Погрузитесь в атмосферу американского джаз-клуба, насладитесь изысканными напитками и закусками, которые предлагает бар «Blue Note».",
      beginTime: "2023-10-29T18:30:00+05:00",
      endTime: "",
      registrationEndTime: "",
      slots: 200,
      extraSlotsPerUser: 2,
      resources: "{}",
    },
    {
      id: 1,
      type: {
        id: 2,
        name: "Мастер-класс"
      },
      name: "Творческий мастер-класс «Роспись по стеклу»",
      organizerName: "Центр декоративно-прикладного искусства «Радуга»",
      locationName: "Ул. Кирова, 34",
      cost: 1200,
      description: "На мастер-классе вы научитесь создавать уникальные рисунки на стеклянных поверхностях при помощи специальных красок и щеток. Опытный преподаватель поможет вам освоить технику росписи по стеклу и создать свой первый шедевр. По окончании мастер-класса вы сможете забрать свою работу с собой.",
      beginTime: "2023-07-21T17:55:00+05:00",
      endTime: "",
      registrationEndTime: "",
      slots: 15,
      extraSlotsPerUser: 0,
      resources: "{}",
    },
    {
      id: 2,
      type: {
        id: 3,
        name: "Выставка"
      },
      name: "Выставка современного искусства «Art Expo»",
      organizerName: "Галерея «ArtMira»",
      locationName: "Ул. Куйбышева 12а",
      cost: null,
      description: "Мероприятие представляет собой выставку современного искусства, где будут представлены работы художников из разных стран. Гости смогут насладиться красотой и оригинальностью работ, а также приобрести их на аукционе.",
      beginTime: "2023-06-13T14:30:00+05:00",
      endTime: "",
      registrationEndTime: "",
      slots: 30,
      extraSlotsPerUser: 1,
      resources: "{}",
    },
    {
      id: 3,
      type: {
        id: 4,
        name: "Фестиваль"
      },
      name: "Фестиваль электронной музыки",
      organizerName: 'Агентство "Rave Time"',
      locationName: "Территория парка им. Горького",
      cost: null,
      description: "Приглашаем всех любителей электронной музыки на наш фестиваль. Вас ждет ночь полная электронных ритмов и ярких впечатлений. Лучшие диджеи страны и мира, световое и звуковое оборудование высшего класса, а также уникальная атмосфера. Мероприятие подойдет для любителей электронной музыки всех возрастов.",
      beginTime: "2023-06-31T21:00:00+05:00",
      endTime: "",
      registrationEndTime: "",
      slots: null,
      extraSlotsPerUser: 0,
      resources: "{}",
    },
    {
      id: 4,
      type: {
        id: 5,
        name: "Мастер-класс"
      },
      name: "Кулинарный мастер-класс",
      organizerName: 'Ресторан "Le Chef"',
      locationName: 'Ул. Грибоедова д.55, кулинарная студия "Cooking Time"',
      cost: 1000,
      description: "Пройдите курс кулинарных мастер-классов от лучших шеф-поваров города. В этот раз мы научим вас готовить блюда французской кухни. Вы получите знания и опыт, которые помогут вам создавать вкусные блюда и удивлять своих близких и друзей. Мастер-класс подойдет для новичков и опытных поваров.",
      beginTime: "2023-05-17T19:20:00+05:00",
      endTime: "",
      registrationEndTime: "",
      slots: 17,
      extraSlotsPerUser: 0,
      resources: "{}",
    }
  ]);

  return (
    <div>
      <h1 class="text-center">
        12 Апреля
      </h1>
      <ListGroup variant="flush">
        {
          events.map((event, index) => (
            <ListGroup.Item className="list-group-item-events">
              <h3>{<NavLink className="navlink-events" to={`${routes.eventRoute}/${event.id}`}>{event.name}</NavLink>}</h3>
              <dl className="dl-close">
              <dd>Тип: {event.type.name}</dd>
              <dd>Место проведения: {event.locationName}</dd>
              <dd>Время начала: {new Date(event.beginTime).toLocaleTimeString().substring(0, 5)}</dd>
              <dd>
                Цена: {event.cost == null ? "Бесплатно" : `${event.cost} ₽`}
                <button>Зарегистрироваться</button>
              </dd>
              </dl>
            </ListGroup.Item>
          ))
        }
      </ListGroup>
    </div>

  );
}

export default Events;