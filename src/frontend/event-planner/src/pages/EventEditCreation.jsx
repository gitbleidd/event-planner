import React, {useState} from "react";
import {Form, } from "react-bootstrap";
import "./EventEditCreation.css";
import FormContainer from "../components/FormContainer";
import Button from "react-bootstrap/Button";
import { CardImage } from "react-bootstrap-icons"
import {useNavigate} from "react-router-dom";
import * as routes from "../shared/routes";
function EventEditCreation() {
  const navigate = useNavigate();
  const [eventTypes, setEventTypes] = useState([
    {id: 1, name: "Концерт"},
    {id: 2, name: "Мастер-класс"},
    {id: 3, name: "Выставка"},
  ]);

  const [validated, setValidated] = useState(false);

  const handleSubmit = (event) => {
    const form = event.currentTarget;
    if (form.checkValidity() === false) {
      event.preventDefault();
      event.stopPropagation();
    }
    setValidated(true);
  };
  
  return (
      <FormContainer>
        <h1 class="text-center h1-form">
          Создание мероприятия
        </h1>
        <Form class="form-center" action={routes.homeRoute} noValidate validated={validated} onSubmit={handleSubmit}>
          <Form.Group>
            <Form.Label>Наименование</Form.Label>
            <Form.Control required type="text" placeholder="Наименование*" />
            <Form.Control.Feedback type="invalid">
              Заполните наименование мероприятия.
            </Form.Control.Feedback>
          </Form.Group>
          <Form.Group >
            <Form.Label>Тип мероприятия</Form.Label>
            <Form.Select required>
              {eventTypes.map(event => (
                  <option value={event.id}>{event.name}</option>
              ))}
            </Form.Select>
            <Form.Control.Feedback type="invalid">
              Заполните тип мероприятия.
            </Form.Control.Feedback>
          </Form.Group>
          <Form.Group>
            <Form.Label>Организатор</Form.Label>
            <Form.Control required type="text" placeholder="Организатор*" />
            <Form.Control.Feedback type="invalid">
              Заполните организатора мероприятия.
            </Form.Control.Feedback>
          </Form.Group>
          <Form.Group>
            <Form.Label>Стоимость</Form.Label>
            <Form.Control type="number" min={1} placeholder="Стоимость" />
            <Form.Control.Feedback type="invalid">
              Стоимость должна быть больше ноля.
            </Form.Control.Feedback>
          </Form.Group>
          <Form.Group>
            <Form.Label>Количество мест</Form.Label>
            <Form.Control type="number" min={1} placeholder="Количество мест" />
            <Form.Control.Feedback type="invalid">
              Количество мест должно быть больше ноля.
            </Form.Control.Feedback>
          </Form.Group>
          <Form.Group>
            <Form.Label>Количество гостей, которых можно пригласить</Form.Label>
            <Form.Control required type="number" min={1} placeholder="Количество гостей, которых можно пригласить" />
            <Form.Control.Feedback type="invalid">
              Заполните количество дополнительных гостей значением больше ноля.
            </Form.Control.Feedback>
          </Form.Group>
          <Form.Group>
            <Form.Label>Дата начала</Form.Label>
            <Form.Control required type="date" placeholder="Дата начала" />
            <Form.Control.Feedback type="invalid">
              Заполните дату начала мероприятия.
            </Form.Control.Feedback>
          </Form.Group>
          <Form.Group>
            <Form.Label>Дата окончания</Form.Label>
            <Form.Control required type="date" placeholder="Дата окончания" />
            <Form.Control.Feedback type="invalid">
              Заполните дату окончания мероприятия.
            </Form.Control.Feedback>
          </Form.Group>
          <Form.Group>
            <Form.Label>Дата окончания регистрации</Form.Label>
            <Form.Control required type="date" placeholder="Дата окончания регистрации" />
            <Form.Control.Feedback type="invalid">
              Заполните дату окончания регистрации на мероприятие.
            </Form.Control.Feedback>
          </Form.Group>
          <Form.Group>
            <Form.Label>Описание</Form.Label>
            <Form.Control required as="textarea" rows={3} placeholder="Описание" />
            <Form.Control.Feedback type="invalid">
              Заполните описание мероприятия.
            </Form.Control.Feedback>
          </Form.Group>
          <Form.Group className="d-flex justify-content-between">
            <Button className="mr-auto p-2 my-4">
              <CardImage className="card-image"/> Добавить изображение
            </Button>
            <Button type="submit" className="p-2 align-self-end my-4 col-3">Создать</Button>
          </Form.Group>
        </Form>
      </FormContainer>
  );
}

export default EventEditCreation;