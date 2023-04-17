import 'bootstrap/dist/css/bootstrap.min.css';
import {Form, Modal} from "react-bootstrap";
import Button from "react-bootstrap/Button";
import 'bootstrap/dist/css/bootstrap.min.css';
import "./SecondaryRoundedButton.css";
import "./PrimaryRoundedButton.css";

function AuthorizationModal(props) {
    return (
        <Modal {...props} aria-labelledby="contained-modal-title-vcenter">
            <Modal.Header closeButton>
                <Modal.Title>Вход</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
                        <Form.Label>Почта</Form.Label>
                        <Form.Control
                            type="email"
                            placeholder="name@example.com"
                        />
                    </Form.Group>
                    <Form.Group
                        className="mb-3"
                        controlId="exampleForm.ControlTextarea1"
                    >
                        <Form.Label>Пароль</Form.Label>
                        <Form.Control
                            type="password"
                        />
                    </Form.Group>
                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary-rounded" onClick={props.onHide}>
                    Отмена
                </Button>
                <Button variant="primary-rounded" onClick={props.onHide}>
                    Войти
                </Button>
            </Modal.Footer>
        </Modal>
    );
}

export default AuthorizationModal;
