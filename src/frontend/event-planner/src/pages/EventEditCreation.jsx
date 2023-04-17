import React, {useState} from "react";
import {Form, } from "react-bootstrap";
import "./EventEditCreation.css";
import FormContainer from "../components/FormContainer";
import Button from "react-bootstrap/Button";
import { CardImage } from "react-bootstrap-icons"
function EventEditCreation() {
  
  const [eventTypes, setEventTypes] = useState([
    {id: 1, name: "Концерт"},
    {id: 2, name: "Мастер-класс"},
    {id: 3, name: "Выставка"},
  ]);
  
  return (

      <FormContainer>
        <h1 class="text-center h1-form">
          Создание мероприятия
        </h1>
        <Form class="form-center">
          <Form.Group>
            <Form.Label>Наименование</Form.Label>
            <Form.Control type="text" placeholder="Наименование*" />
          </Form.Group>
          <Form.Group >
            <Form.Label>Тип мероприятия</Form.Label>
            <Form.Select>
              {eventTypes.map(event => (
                  <option value={event.id}>{event.name}</option>
              ))}
            </Form.Select> 
          </Form.Group>
          <Form.Group>
            <Form.Label>Организатор</Form.Label>
            <Form.Control type="text" placeholder="Организатор*" />
          </Form.Group>
          <Form.Group>
            <Form.Label>Стоимость</Form.Label>
            <Form.Control type="number" placeholder="Стоимость" />
          </Form.Group>
          <Form.Group>
            <Form.Label>Количество мест</Form.Label>
            <Form.Control type="number" placeholder="Количество мест" />
          </Form.Group>
          <Form.Group>
            <Form.Label>Количество гостей, которых можно пригласить</Form.Label>
            <Form.Control type="number" placeholder="Количество гостей, которых можно пригласить" />
          </Form.Group>
          <Form.Group>
            <Form.Label>Дата начала</Form.Label>
            <Form.Control type="date" placeholder="Дата начала" />
          </Form.Group>
          <Form.Group>
            <Form.Label>Дата окончания</Form.Label>
            <Form.Control type="date" placeholder="Дата окончания" />
          </Form.Group>
          <Form.Group>
            <Form.Label>Дата окончания регистрации</Form.Label>
            <Form.Control type="date" placeholder="Дата окончания регистрации" />
          </Form.Group>
          <Form.Group>
            <Form.Label>Описание</Form.Label>
            <Form.Control as="textarea" rows={3} placeholder="Описание" />
          </Form.Group>
          <Form.Group className="d-flex justify-content-between">
            <Button className="mr-auto p-2 my-4">
              <CardImage className="card-image"/> Добавить изображение
            </Button>
            <Button className="p-2 align-self-end my-4 col-3">Создать</Button>
          </Form.Group>
          
        </Form>
      </FormContainer>
  );
}

export default EventEditCreation;