from flask import Flask, render_template
from flask_wtf import FlaskForm
from flask_codemirror.fields import CodeMirrorField
from wtforms.fields import SubmitField
from flask_codemirror import CodeMirror
from flask_bootstrap import Bootstrap
import subprocess
import json

SECRET_KEY = 'secret!'
CODEMIRROR_LANGUAGES = ['clike', 'html']
CODEMIRROR_THEME = 'mdn-like'
CODEMIRROR_ADDONS = (
     ('display','placeholder'),
)

app = Flask(__name__)
app.config.from_object(__name__)
codemirror = CodeMirror(app)
Bootstrap(app)

class MyForm(FlaskForm):
    source_code = CodeMirrorField(language='clike',
                                config={'lineNumbers' : 'true'})
    submit = SubmitField('Submit')

@app.route('/', methods = ['GET', 'POST'])
def index():
    form = MyForm()
    
    if form.source_code.data == None:
        sample = open('sampleToAnalize.cs', 'r')
        form.source_code.data = sample.read()

    csharp_output_json = None
    if form.validate_on_submit():
        inputText = form.source_code.data

        with open('FormInput.cs', 'w', encoding = 'utf-8') as f:
            text = inputText.replace('\r\n', '\n')
            f.write(text)
        
        output = subprocess.check_output(["./c# project/RoslynAnalyzer/bin/Debug/RoslynAnalyzer.exe", "FormInput.cs"]).decode("utf-8")

        csharp_output_string = output
        
        csharp_output_json = json.loads(csharp_output_string)
        
    return render_template('index.html', form=form, Issues = csharp_output_json)

@app.route('/aboutUs', methods=['GET', ])
def about_us():
    return render_template('about_us.html')

@app.route('/this-site', methods=['GET', ])
def about_site():
    return render_template('about_site.html')