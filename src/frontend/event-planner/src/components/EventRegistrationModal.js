import 'bootstrap/dist/css/bootstrap.min.css';
import {Form, Modal} from "react-bootstrap";
import Button from "react-bootstrap/Button";
import 'bootstrap/dist/css/bootstrap.min.css';
import "./SecondaryRoundedButton.css";
import "./PrimaryRoundedButton.css";

function EventRegistrationModal(props) {
  return (
    <Modal {...props} aria-labelledby="contained-modal-title-vcenter">
      <Modal.Header closeButton>
        <Modal.Title>Регастрация на мероприятие</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Form>
          <Form.Group className="mb-2" controlId="exampleForm.ControlInput1">
            <Form.Label>Почта</Form.Label>
            <Form.Control
              type="email"
              placeholder="name@example.com"
            />
          </Form.Group>

          <Form.Group
            className="mb-2"
          >
            <Form.Label>Фамилия</Form.Label>
            <Form.Control type="text" placeholder="Фамилия*" />
          </Form.Group>
          <Form.Group
            className="mb-2"
          >
            <Form.Label>Имя</Form.Label>
            <Form.Control type="text" placeholder="Имя*" />
          </Form.Group>
          <Form.Group
            className="mb-2"
          >
            <Form.Label>Отчество</Form.Label>
            <Form.Control type="text" placeholder="Отчество" />
          </Form.Group>
        </Form>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary-rounded" onClick={props.onHide}>
          Отмена
        </Button>
        <Button variant="primary-rounded" onClick={props.onHide}>
          Зарегистрироваться
        </Button>
      </Modal.Footer>
    </Modal>
  );
}

export default EventRegistrationModal;
