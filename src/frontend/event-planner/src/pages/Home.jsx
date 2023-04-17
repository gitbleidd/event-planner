import { useState } from 'react'
import { useNavigate } from "react-router-dom"
import FullCalendar from '@fullcalendar/react'
import dayGridPlugin from '@fullcalendar/daygrid'
import interactionPlugin from "@fullcalendar/interaction"
import allLocales from "@fullcalendar/core/locales-all"
import "./Home.css"

function Home() {
  const navigate = useNavigate();
  const [events, setEvents] = useState([
    { id: 1, title: 'Концерт "Музыка детям"', date: '2023-04-01',},
    { id: 2, title: 'Арфовый концерт', date: '2023-04-01' },
    { id: 3, title: 'Dragonaut Fest', date: '2023-04-01' },
    { id: 4, title: 'Состояние Пасхи', date: '2023-04-01' },
    { id: 5, title: 'Выставка «Яхтинг — это»', date: '2023-04-10' },
    { id: 6, title: 'И я, как Фрида... - выставка', date: '2023-04-20' },
    { id: 7, title: '"Квадрат. Тема и вариации"', date: '2023-03-30' },
    { id: 8, title: '20-й Московский Фестиваль Татуировки', date: '2023-04-20' },
  ]);

  return (
    <FullCalendar
      plugins={[ dayGridPlugin, interactionPlugin ]}
      headerToolbar={{
        start: "",
        center: "title",
        end: "prev,next"
      }}
      selectable={true}
      height={"auto"}
      locale="ru"
      locales={allLocales}
      firstDay={1}
      dayMaxEvents={2}
      events={events}
      dateClick={(dateClick) => navigate("/events", { state: dateClick.date })}
      eventClick={(eventClick) => navigate(`/events/${eventClick.event.id}`)}
    />
  );
}

export default Home;