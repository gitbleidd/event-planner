import React, { useState } from "react";
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Button from 'react-bootstrap/Button';
import Carousel from 'react-bootstrap/Carousel';
import "./Event.css"

function Event() {
  const [event, setEvent] = useState({
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
    resources: JSON.stringify({
      imgs: [
        "https://www.gannett-cdn.com/-mm-/b3c10c2535284d4b509bc078a90a370e2e2c2fca/c=0-741-3913-3683/local/-/media/2015/07/16/MIGroup/Lansing/635726564289570272-ThinkstockPhotos-486813379-1-.jpg?width=2560",
        "https://myvancity.ca/wp-content/uploads/2019/06/pixabay-jazz-3.jpg",
        "https://api-cdn.arte.tv/img/v2/image/Evw4kjDHWh44LfF9pD9RY8/1920x1080"
      ]
    }),
  });
  const [resources, setResources] = useState(event ? JSON.parse(event.resources) : null);

  const renderCarousel = () => {
    if (resources === null || resources === undefined) {
      return <></>;
    }

    return resources.imgs.map((imgUrl, index) => (
      <Carousel.Item key={index}>
        <img
          className="d-block w-100"
          src={imgUrl}
          alt={`${index} изображение`}
        />
      </Carousel.Item>
    ));
  }

  return (
    <Container>
      <Row>{<h1>{event.name}</h1>}</Row>
      <Row className="mb-3 mb-md-0">

        <Col md="10">
          <dl className="dl-close">
            <dd>Тип: {event.type.name}</dd>
            <dd>Организатор: {event.organizerName}</dd>
            <dd>Место проведения: {event.locationName}</dd>
            <dd>
              Цена: {event.cost == null ? "Бесплатно" : `${event.cost} ₽`}
            </dd>
          </dl>
        </Col>
        <Col>
          <Row className="d-flex justify-content-end" style={{ marginBottom: '20px' }}>
            <Button variant="primary-rounded">Все участники</Button>
          </Row>
          <Row className="d-flex justify-content-end">
            <Button variant="primary-rounded">Фактические участники</Button>
          </Row>
        </Col>
      </Row>

      <Row>
        <Col md="9"><p>{event.description}</p></Col>
      </Row>

      <Row>
        <Col className="d-flex justify-content-center">
          <Carousel>
            {renderCarousel()}
          </Carousel>
        </Col>
      </Row>

      <div className="border-top my-3"></div>
      <Row className="mb-5">
        <Col className="d-flex align-items-center">
          <h3>
            {new Date(event.beginTime).toLocaleString('ru', {
              month: 'long',
              day: 'numeric',
              hour: 'numeric',
              minute: 'numeric'
            })}

          </h3>
        </Col>

        <Col className="d-flex justify-content-center justify-content-md-end">
          <Button variant="primary-rounded" className="me-xs-0 mx-sm-2 ">Зарегистрироваться</Button>
          {/* <Button variant="primary-rounded" className="me-xs-0 mx-sm-2 ">Удалить</Button>
          <Button variant="primary-rounded" className="me-xs-0 mx-sm-2 ">Редактировать</Button> */}
        </Col>
      </Row>
    </Container>
  );
}

export default Event;